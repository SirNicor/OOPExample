using System.Data;
using System.Data.SqlClient;
using Dapper;
using Logger;
using UCore;

namespace Repository;

public class RoleRepository : IRoleRepository
{
    private readonly string _connectionString;
    private readonly MyLogger _logger;

    public RoleRepository(IGetConnectionString getConnectionString, MyLogger logger)
    {
        _connectionString = getConnectionString.ReturnConnectionString();
        _logger = logger;
    }

    public RoleAccessDto GetRoleAccess(int roleId)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        const string sql = @"
            SELECT 
                r.Id AS Id,
                r.Name AS NameRole,
                tor.Name AS TypeOperation,
                ap.Name AS AccessPage
            FROM Role r
            INNER JOIN RoleAccess ra ON r.Id = ra.IdRole
            INNER JOIN TypeOperationRole tor ON ra.IdTypeOperation = tor.Id
            INNER JOIN AccessPage ap ON ra.IdAccessPage = ap.Id
            WHERE r.Id = @roleId";

        try
        {
            return db.QueryFirstOrDefault<RoleAccessDto>(sql, new { roleId });
        }
        catch (Exception ex)
        {
            _logger.Error($"Error getting role access for RoleId {roleId}: {ex.Message}");
            throw;
        }
    }
}