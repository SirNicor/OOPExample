using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using Logger;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Repository;
using UCore;


public class AuthorizationRepository : IAuthorizationRepository
{
    private string _connectionString;
    private MyLogger _logger;

    public AuthorizationRepository(IGetConnectionString getConnectionString, MyLogger logger)
    {
        _connectionString = getConnectionString.ReturnConnectionString();
        _logger = logger;
    }
    
    public AuthorizationDto GetForLoginAuthorization(string login)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            const string sql = @"
                SELECT 
                    Id,
                    Login,
                    Email,
                    HashPassword AS Password,
                    Salt,
                    Phone,
                    Role,
                    IdAdmin,
                    IdTeacher,
                    BlackList
                FROM AuthorizationTable
                WHERE Login = @login";
            
            return db.QueryFirstOrDefault<AuthorizationDto>(sql, new { login });
        }
    }

    public AuthorizationDto GetForIdAuthorization(long id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            const string sql = @"
                SELECT 
                    Id,
                    Login,
                    Email,
                    HashPassword AS Password,
                    Salt,
                    Phone,
                    Role,
                    IdAdmin,
                    IdTeacher,
                    BlackList
                FROM AuthorizationTable
                WHERE Id = @id";

            return db.QueryFirstOrDefault<AuthorizationDto>(sql, new { id });
        }
    }

    public long CreateAuthorization(AuthorizationDto dto)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
                    dto.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: dto.Password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 256 / 8));
                    dto.Salt = Convert.ToBase64String(salt);
                    
                    const string sql = @"
                        INSERT INTO AuthorizationTable (
                            Login,
                            Email,
                            HashPassword,
                            Salt,
                            Phone,
                            Role,
                            IdAdmin,
                            IdTeacher,
                            BlackList
                        ) VALUES (
                            @Login,
                            @Email,
                            @Password,
                            @Salt,
                            @Phone,
                            @Role,
                            @IdAdmin,
                            @IdTeacher,
                            @BlackList
                        );
                        SELECT SCOPE_IDENTITY();";

                    var id = db.ExecuteScalar<long>(sql, dto, transaction);
                    transaction.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.Error($"Error creating authorization: {ex.Message}");
                    throw;
                }
            }
        }
    }

    public long UpdateAuthorization(AuthorizationDto dto)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
                    dto.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: dto.Password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 256 / 8));
                    dto.Salt = Convert.ToBase64String(salt);
                    
                    const string sql = @"
                        UPDATE AuthorizationTable 
                        SET 
                            Email = @Email,
                            HashPassword = @Password,
                            Salt = @Salt,
                            Phone = @Phone,
                            Role = @Role,
                            IdAdmin = @IdAdmin,
                            IdTeacher = @IdTeacher,
                            BlackList = @BlackList
                        WHERE Login = @Login";

                    var rowsAffected = db.Execute(sql, dto, transaction);
                    transaction.Commit();
                    return dto.Id ?? 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.Error($"Error updating authorization: {ex.Message}");
                    throw;
                }
            }
        }
    }

    public int? GetAuthorizationsRoleForIndex(AuthorizationForGetJwtToken dto)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            const string sql = @"
                SELECT Role
                FROM AuthorizationTable
                WHERE Login = @login AND
                      Email = @email AND
                      Phone = @phone";

            return db.QueryFirstOrDefault<int>(sql, dto);
        }
    }

    public bool CheckPassword(string password, long id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            const string sql = @"
                SELECT HashPassword AS Password, Salt
                FROM AuthorizationTable
                WHERE Id = @id";
            
            PasswordAndSalt? passwordAndSalt = db.QueryFirstOrDefault<PasswordAndSalt>(sql, new { id });
            if (passwordAndSalt is null)
            {
                return false;
            }
            
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(passwordAndSalt.Salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            
            return hashedPassword == passwordAndSalt.Password;
        }
    }

    public RefreshJWTTokenDTO GetJWTToken(string token)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            try
            {
                using var sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
                string tokenHash = Convert.ToBase64String(hashBytes);
                
                const string sql = @"
                    SELECT Id, Token, RevokedAt, IdAuthorizationTable 
                    FROM RefreshJWTToken 
                    WHERE Token = @token";
                
                var jwtToken = db.QueryFirstOrDefault<RefreshJWTTokenDTO>(sql, new { token = tokenHash });
                return jwtToken ?? new RefreshJWTTokenDTO();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error getting token by signature {token}: {ex.Message}");
                throw;
            }
        }
    }

    public long? CheckAndUpdateJWTToken(string token)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    using var sha256 = SHA256.Create();
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
                    string tokenHash = Convert.ToBase64String(hashBytes);
                    
                    const string selectSql = @"
                        SELECT Id, Token, RevokedAt, IdAuthorizationTable 
                        FROM RefreshJWTToken 
                        WHERE Token = @token";
                    
                    var existingToken = db.QueryFirstOrDefault<RefreshJWTTokenDTO>(
                        selectSql,
                        new { token = tokenHash },
                        transaction
                    );
                    
                    if (existingToken != null && !existingToken.RevokedAt)
                    {
                        const string updateSql = @"
                            UPDATE RefreshJWTToken 
                            SET RevokedAt = 1 
                            WHERE Id = @Id";
                        db.Execute(updateSql, new { existingToken.Id }, transaction);
                        transaction.Commit();
                        return existingToken.IdAuthorizationTable;
                    }
                    
                    transaction.Commit();
                    return null;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.Error($"Error checking and updating token: {ex.Message}");
                    throw;
                }
            }
        }
    }

    public long CreateJWTToken(RefreshJWTTokenDTO dto)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            try
            {
                using var sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(dto.Token));
                string tokenHash = Convert.ToBase64String(hashBytes);
                
                const string sql = @"
                    INSERT INTO RefreshJWTToken (Token, IdAuthorizationTable, RevokedAt) 
                    VALUES (@Token, @IdAuthorizationTable, 0);
                    SELECT SCOPE_IDENTITY();";
                
                var id = db.QuerySingle<long>(sql, new { Token = tokenHash, dto.IdAuthorizationTable });
                return id;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error creating token: {ex.Message}");
                throw;
            }
        }
    }

    public void DeleteJWTToken(string token)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            try
            {
                using var sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
                string tokenHash = Convert.ToBase64String(hashBytes);
                
                const string sql = "DELETE FROM RefreshJWTToken WHERE Token = @token";
                var affectedRows = db.Execute(sql, new { token = tokenHash });
                
                if (affectedRows == 0)
                {
                    _logger.Warning($"No token found with signature: {token}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error deleting token: {ex.Message}");
                throw;
            }
        }
    }
}