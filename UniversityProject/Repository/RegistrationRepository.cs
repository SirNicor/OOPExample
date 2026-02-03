using  UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Repository;

public class RegistrationRepository : IRegistrationRepository
{
    private string _connectionString;
    private MyLogger _logger;
    
    public RegistrationRepository(IGetConnectionString getConnectionString, MyLogger logger)
    {
        _connectionString = getConnectionString.ReturnConnectionString();
        _logger = logger;
    }
    public RegistrationDTO Get(string login)
    {
        var sqlQuery = "SELECT RAT.Id, RAT.Login, RAT.Email, RAT.Password, RAT.Phone FROM RegistrationAdminTable RAT WHERE Login = @login";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var regDTO = db.Query<RegistrationDTO>(sqlQuery, new { login }).FirstOrDefault();
            return regDTO;
        }
    }

    public long Create(RegistrationDTO registration)
    {
        var sqlQuery = @"INSERT INTO RegistrationAdminTable(Login, Email, Password, Phone)
        VALUES(@Login, @Email, @Password, @Phone);
        SELECT SCOPE_IDENTITY();";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    var id = db.QuerySingle<long>(sqlQuery, registration, transaction);
                    transaction.Commit();
                    return id;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    transaction.Rollback();
                    throw;
                }
                
            }   
        }
    }

    public long Update(RegistrationDTO registration)
    {
        var sqlQuery = @"UPDATE RegistrationAdminTable
        SET Login = @Login, Email = @Email, Password = @Password, Phone = @Phone
        WHERE Id = @Id";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    var id = db.Execute(sqlQuery, registration, transaction);
                    transaction.Commit();
                    return id;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    transaction.Rollback();
                    throw;
                }
                
            }   
        }
    }
}