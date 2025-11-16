namespace Repository;
using UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;
public class UniversityRepository : IUniversityRepository
{
    private const string SqlSelectUniversityQuery = @"SELECT un.ID as universityId, un.NameUniversity, un.Budget, ad.Id as PersonId, ad.Salary, ad.CriminalRecord,
ad.MilitaryID, ad.PassportID, p.Serial, p.Number, p.FirstName, p.LastName,
p.MiddleName, p.BirthData, p.AddressId, a.Country, a.City, a.Street, a.HouseNumber FROM University un
JOIN Administrator ad ON ad.Id = un.Rector
INNER JOIN Passport p ON ad.PassportId = p.ID
INNER JOIN Address a ON p.AddressId = a.ID
INNER JOIN IdMilitary im ON ad.MilitaryId = im.ID ";
    private const string SqlSelectPersonalOfAdministratorQuery = @"SELECT PU.IdUniversity, ad.Id as PersonId, ad.Salary, ad.CriminalRecord,
ad.MilitaryID, ad.PassportID, p.Serial, p.Number, p.FirstName, p.LastName,
p.MiddleName, p.BirthData, p.AddressId, a.Country, a.City, a.Street, a.HouseNumber FROM PersonalOfUniversity PU
JOIN Administrator ad ON ad.Id = PU.IdAdministrator
INNER JOIN Passport p ON ad.PassportId = p.ID
INNER JOIN Address a ON p.AddressId = a.ID
INNER JOIN IdMilitary im ON ad.MilitaryId = im.ID";
    
    private const string SqlSelectIdUniversityQuery = @"Select 
    un.Id AS ID
    FROM University un";
    public UniversityRepository(IGetConnectionString getConnectionString, MyLogger logger)
    {
        _connectionString =  getConnectionString.ReturnConnectionString();
        _myLogger = logger;
    }

    public University Get(long ID)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            List<Administrator> administrators = db.Query<Administrator, Passport, Address, Administrator>(
                SqlSelectPersonalOfAdministratorQuery + @"WHERE IdUniversity = @ID", 
                (administrator, passport, address) =>
                {
                    passport.Address = address;
                    administrator.Passport = passport;
                    return administrator;
                },
                new { ID }, splitOn: "PassportId, AddressId").ToList();
            University university = db.Query<University>(SqlSelectUniversityQuery + @"WHERE un.ID = @ID", new { ID }).First();
            university.Administrators = administrators;
            return university;
        }
    }

    public List<University> ReturnList()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            List<University> universities = db.Query<University>(SqlSelectUniversityQuery).ToList();
            var personal = db.Query<PersonalOfUniversiyDTO, Administrator, Passport, Address, PersonalOfUniversiyDTO>(
                SqlSelectPersonalOfAdministratorQuery, 
                (personalOfUniversity, administrator, passport, address) =>
                {
                    passport.Address = address;
                    administrator.Passport = passport;
                    personalOfUniversity.Administrator = administrator;
                    return personalOfUniversity;
                }, splitOn: "PersonId, PassportId, AddressId").ToList();
            
            var personalOfUniversity = personal
                .GroupBy(PoF => PoF.IdUniversity)
                .ToDictionary(x => x.Key, 
                    x => x.Select(PoF => PoF.Administrator).ToList());
            foreach (var university in universities)
            {
                university.Administrators = personalOfUniversity.GetValueOrDefault(university.UniversityId);
            }
            return universities;
        }
    }
    public long Create(UniversityDto university)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                string SqlQuery;
                try
                {
                    SqlQuery = @"INSERT INTO UNIVERSITY(NameUniversity, Budget) VALUES(@NameUniversity, @BudgetSize);
                    SELECT SCOPE_IDENTITY();";
                    university.IdUniversity = db.QuerySingle<int>(SqlQuery, university, transaction);
                    var admin = university.IdAdministrators.Select(adminId => new
                    {
                        IdUniversity = university.IdUniversity,
                        IdAdministrators = adminId
                    }).ToList();
                    SqlQuery = @"INSERT INTO PersonalOfUniversity(IdUniversity, IdAdministrator) VALUES(@IdUniversity, @IdAdministrators)";
                    db.Execute(SqlQuery,  admin , transaction);
                    transaction.Commit();
                    return university.IdUniversity;
                }
                catch (Exception ex)
                {
                    _myLogger.Error("An error occured during transaction" + ex.Message);
                    transaction.Rollback();
                    throw;
                }
            }
            
        }
    }

    public long Update(UniversityDto university)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                string SqlQuery;
                try
                {
                    SqlQuery = @"UPDATE UNIVERSITY SET NameUniversity = @NameUniversity, Budget = @Budget WHERE ID = @IdUniversity";
                    db.Execute(SqlQuery, university, transaction);
                    SqlQuery = @"DELETE FROM PersonalOfUniversity WHERE IdUniversity = @IdUniversity";
                    db.Execute(SqlQuery, university, transaction);
                    var admin = university.IdAdministrators.Select(adminId => new
                    {
                        IdUniversity = university.IdUniversity,
                        IdAdministrators = adminId
                    }).ToList();
                    SqlQuery = @"INSERT INTO PersonalOfUniversity(IdUniversity, IdAdministrator) VALUES(@IdUniversity, @IdAdministrators)";
                    db.Execute(SqlQuery,  admin , transaction);
                    transaction.Commit();
                    return university.IdUniversity;
                }
                catch (Exception ex)
                {
                    _myLogger.Error("An error occured during transaction" + ex.Message);
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
    public void Delete(long ID)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    string SqlQuery = @"DELETE FROM University WHERE ID = @ID";
                    db.Execute(SqlQuery, new { ID },  transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _myLogger.Error("An error occured during transaction" + ex.Message);
                    transaction.Rollback();
                }
            }   
        }
    }
    
    private string _connectionString = null;
    private MyLogger _myLogger;
}