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
    public class UserStateRepository : IUserStateRepository
    {
        private const string _sqlQuery = @"SELECT us.Id as ChatId, us.ListUserStateId, LUS.UserStatus AS UserState, 
us.UniversityId, us.FacultyId, us.DepartmentId, us.DirectionId,
us.StudentId, us.LastName AS StudentLastName, us.FirstName AS StudentFirstName
FROM UserState us
JOIN ListUserState LUS ON LUS.Id = us.ListUserStateId
WHERE us.Id = @Id";
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
                            VALUES(@ChatId, (SELECT Id FROM ListUserState WHERE UserStatus = @UserState), @UniversityId, 
                            @FacultyId, @DepartmentId, @DirectionId, @StudentId, @StudentFirstName, @StudentLastName)";
                        db.Execute(SqlQuery, new
                        {
                            registration.ChatId, UserState =registration.UserState.ToString(), registration.UniversityId,
                            registration.FacultyId, registration.DepartmentId, registration.DirectionId, registration.StudentId,
                            registration.StudentFirstName, registration.StudentLastName
                        }
                            , transaction);

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

        public UserStateRegistration? Get(long Id)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var UserState = db.Query<UserStateRegistration>(_sqlQuery, new { Id }).FirstOrDefault();
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
                        SET ListUserStateId = (SELECT Id FROM ListUserState WHERE UserStatus = @UserState), 
                        UniversityId = @UniversityId, FacultyId = @FacultyId, DepartmentId = @DepartmentId,
                        DirectionId = @DirectionId, StudentId = @StudentId, FirstName = @StudentFirstName, LastName = @StudentLastName;";
                        db.Execute(SqlQuery, new
                        {
                            registration.ChatId,
                            UserState = registration.UserState.ToString(),
                            registration.UniversityId,
                            registration.FacultyId,
                            registration.DepartmentId,
                            registration.DirectionId,
                            registration.StudentId,
                            registration.StudentFirstName,
                            registration.StudentLastName
                        }, transaction);
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
