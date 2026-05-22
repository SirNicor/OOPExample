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
            var rawResults = db.Query<RoleAccessRaw>(sql, new { roleId }).ToList();
            
            if (rawResults.Count == 0)
                return null;

            // Формируем массив кортежей (операция, страница) для каждой записи
            var permissions = rawResults
                .Select(x => Tuple.Create(x.TypeOperation, x.AccessPage))
                .Distinct()
                .ToArray();

            return new RoleAccessDto
            {
                Id = rawResults[0].Id,
                NameRole = rawResults[0].NameRole,
                TypeOperationAccessPage = permissions
            };
        }
        catch (Exception ex)
        {
            _logger.Error($"Error getting role access for RoleId {roleId}: {ex.Message}");
            throw;
        }
    }

    public RoleAccessDto[] GetRoleAccess(int[] rolesId)
    {
        if (rolesId == null || rolesId.Length == 0)
            return Array.Empty<RoleAccessDto>();

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
            WHERE r.Id IN @rolesId";

        try
        {
            var rawResults = db.Query<RoleAccessRaw>(sql, new { rolesId }).ToList();
            
            if (rawResults.Count == 0)
                return Array.Empty<RoleAccessDto>();

            // Группируем по роли и формируем кортежи для каждой
            var result = rawResults
                .GroupBy(x => new { x.Id, x.NameRole })
                .Select(g => new RoleAccessDto
                {
                    Id = g.Key.Id,
                    NameRole = g.Key.NameRole,
                    TypeOperationAccessPage = g
                        .Select(x => Tuple.Create(x.TypeOperation, x.AccessPage))
                        .Distinct()
                        .ToArray()
                })
                .ToArray();

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error getting role access for roles [{string.Join(",", rolesId)}]: {ex.Message}");
            throw;
        }
    }
}