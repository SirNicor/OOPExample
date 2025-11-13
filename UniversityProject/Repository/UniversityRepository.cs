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
INNER JOIN IdMilitary im ON ad.MilitaryId = im.ID ";
    
    private const string SqlSelectIdUniversityQuery = @"Select 
    un.Id AS ID
    FROM University un";
    public UniversityRepository(IGetConnectionString getConnectionString, MyLogger logger, IWorkerAdministratorRepository workerAdministratorRepository)
    {
        _connectionString =  getConnectionString.ReturnConnectionString();
        _myLogger = logger;
        _workerAdministratorRepository = workerAdministratorRepository;
    }

    public University Get(int ID)
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
            University university = db.Query<University, Administrator, Passport, Address, University>(SqlSelectUniversityQuery + @"WHERE un.ID = @ID",
                (university, administrator, passport, address) =>
                {
                    passport.Address = address;
                    administrator.Passport = passport;
                    university.Rector = administrator;
                    return university;
                }, new { ID }, splitOn: "PersonId, PassportId, AddressId").First();
            university.Administrators = administrators;
            return university;
        }
    }

    public List<University> ReturnList()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            List<int> idUniversity = db.Query<int>(SqlSelectIdUniversityQuery).ToList();
            List<University> universities = db.Query<University, Administrator, Passport, Address, University>(
                SqlSelectUniversityQuery,
                (university, administrator, passport, address) =>
                {
                    passport.Address = address;
                    administrator.Passport = passport;
                    university.Rector = administrator;
                    return university;
                }, splitOn: "PersonId, PassportId, AddressId").ToList();
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
                .ToDictionary(x => x.Key, x => x.Select(PoF => PoF.Administrator).ToList());
            foreach (var university in universities)
            {
                university.Administrators = personalOfUniversity.GetValueOrDefault(university.UniversityId);
            }
            /*List<University> universities = new List<University>();
            foreach(int id in idUniversity)
            {
                List<Administrator> administrators = db.Query<Administrator, Passport, Address, Administrator>(
                    SqlSelectPersonalOfAdministratorQuery + @"WHERE IdUniversity = @id", 
                    (administrator, passport, address) =>
                    {
                        passport.Address = address;
                        administrator.Passport = passport;
                        return administrator;
                    },
                    new { id }, splitOn: "PassportId, AddressId").ToList();
                University university = db.Query<University, Administrator, Passport, Address, University>(
                    SqlSelectUniversityQuery + @"WHERE un.ID = @id",
                    (university, administrator, passport, address) =>
                    {
                        passport.Address = address;
                        administrator.Passport = passport;
                        university.Rector = administrator;
                        return university;
                    }, new { id }, splitOn: "AdministratorId, PassportId, AddressId").First();
                university.Administrators = administrators;
                universities.Add(university);
            }*/
            return universities;
        }
    }
    public int Create(UniversityDto university)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                string SqlQuery;
                try
                {
                    SqlQuery = @"INSERT INTO UNIVERSITY(NameUniversity, Rector, Budget) VALUES(@NameUniversity, @IdRector, @BudgetSize);
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

    public int Update(UniversityDto university)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                string SqlQuery;
                try
                {
                    SqlQuery = @"UPDATE UNIVERSITY SET NameUniversity = @NameUniversity, Rector = @IdRector, Budget = @NameUniversity WHERE ID = @IdUniversity";
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
                    _myLogger.Info("Successfully updated universities");
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
    public void Delete(int ID)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            List<int> IdAdministrator =  db.Query<int>(SqlSelectPersonalOfAdministratorQuery + " WHERE IDUniversity = @ID", new { ID }).ToList();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    string SqlQuery = @"DELETE FROM University WHERE ID = @ID";
                    db.Execute(SqlQuery, new { ID },  transaction);
                    transaction.Commit();
                    _myLogger.Info("Deleted University. ID = " + ID);
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
    private IWorkerAdministratorRepository _workerAdministratorRepository;
}