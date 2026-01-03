using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCore;

namespace Repository
{
    internal class UserStateRepository : IUserStateRepository
    {
        private const string _sqlQuery = "SELECT * FROM UserState WHERE Id = @Id";
        private string _connectionString;
        public UserStateRepository(IGetConnectionString getConnectionString)
        {
            _connectionString = getConnectionString.ReturnConnectionString();
        }
        public long Create(UserStateRegistration registration)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using(IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        string SqlQuery = @"INSERT INTO UserState(Id, ListUserStateId, 
                            UniversityId, FacultyId, DepartmentId, DirectionId, StudentId, FirstName, LastName)
                            VALUES(@ChatId, (SELECT Id FROM ListUserState WHERE Status = @UserState), @UniversityId, 
                            @FacultyId, @DepartmentId, @DirectionId, @StudentId, @FirstName, @LastName)";
                        db.Execute(SqlQuery, registration, transaction);
                        transaction.Commit();
                        return registration.ChatId;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public UserStateRegistration Get(long Id)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var UserState = db.Query<UserStateRegistration>(_sqlQuery, new { Id }).First();
                return UserState;
            }
        }

        public long Update(UserStateRegistration registration)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        string SqlQuery = @"UPDATE UserState 
                        SET Id = @ChatId, ListUserStateId = (SELECT Id FROM ListUserState WHERE Status = @UserState), 
                        UniversityId = @UniversityId, FacultyId = @FacultyId, DepartmentId = @DepartmentId,
                        DirectionId = @DirectionId, StudentId = @StudentId, FirstName = @FirstName, LastName = @LastName;";
                        db.Execute(SqlQuery, registration, transaction);
                        transaction.Commit();
                        return registration.ChatId;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
