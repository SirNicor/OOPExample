namespace Repository;
using UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;

public class FacultyRepository : IFacultyRepository
{
    private const string SqlSelectFacultyQuery = @"SELECT fc.ID AS FacultyId, fc.NameFaculty, fc.IdUniversity AS UniversityId, un.Budget, un.Budget FROM Faculty fc 
JOIN University un ON un.Id = fc.IdUniversity ";

    private const string SqlSelectAdministratorOfFaculty =
        @"SELECT ADO.IdFaculty AS FacultyId, ad.Id as PersonId, ad.Salary, ad.CriminalRecord,
        ad.MilitaryID, ad.PassportID, p.Serial, p.Number, p.FirstName, p.LastName,
        p.MiddleName, p.BirthData, p.AddressId, a.Country, a.City, a.Street, a.HouseNumber FROM AdministrationOfFaculty ADO
        JOIN Administrator ad ON ad.Id = ADO.IdAdministrator
        INNER JOIN Passport p ON ad.PassportId = p.ID
        INNER JOIN Address a ON p.AddressId = a.ID
        INNER JOIN IdMilitary im ON ad.MilitaryId = im.ID ";
    private MyLogger _myLogger;
    private string _connectionString;
    public FacultyRepository(IGetConnectionString getConnectionString, MyLogger logger)
    {
        _connectionString =  getConnectionString.ReturnConnectionString();
        _myLogger = logger;
    }

    public long Create(FacultyDto faculty)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    string SqlQuery;
                    SqlQuery = @"INSERT INTO Faculty(NameFaculty, IdUniversity) VALUES(@NameFaculty, @IdUniversity);
                    SELECT SCOPE_IDENTITY();";
                    faculty.IdFaculty = db.QuerySingle<int>(SqlQuery, faculty, transaction);
                    var admin = faculty.IdAdministrators.Select(adminId => new
                    {
                        IdFaculty = faculty.IdFaculty,
                        IdAdministrators = adminId
                    }).ToList();
                    SqlQuery = @"INSERT INTO AdministrationOfFaculty(IdFaculty, IdAdministrator) VALUES(@IdFaculty, @IdAdministrators)";
                    db.Execute(SqlQuery,  admin , transaction);
                    transaction.Commit();
                    return faculty.IdFaculty;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }

    public Faculty Get(long Id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            List<Administrator> administrators = db.Query<Administrator, Passport, Address, Administrator>(
                SqlSelectAdministratorOfFaculty + @"WHERE IdFaculty = @Id",
                (administrator, passport, address) =>
                {
                    passport.Address = address;
                    administrator.Passport = passport;
                    return administrator;
                },
                new { Id }, splitOn: "PassportId, AddressId").ToList();
            Faculty faculty = db.Query<Faculty, University, Faculty>(SqlSelectFacultyQuery + @"WHERE fc.Id = @Id",
                (faculty, university) =>
                {
                    faculty.University = university;
                    return faculty;
                }, new{Id}, splitOn: "UniversityId").First();
            faculty.AdministrationOfFaculty = administrators;
            return faculty;
        }
    }

    public List<Faculty> ReturnList()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            List<Faculty> faculties = db.Query<Faculty, University, Faculty>(SqlSelectFacultyQuery,
                (faculty, university) =>
                {
                    faculty.University = university;
                    return faculty;
                }, splitOn: "UniversityId").ToList();
            List<AdministrationOfFacultyDto> administrators = db.Query<AdministrationOfFacultyDto, Administrator, Passport, Address, AdministrationOfFacultyDto>(
                SqlSelectAdministratorOfFaculty,    
                (administrationOfFacultyDto, administrator, passport, address) =>
                {
                    passport.Address = address;
                    administrator.Passport = passport;
                    administrationOfFacultyDto.Administrator = administrator;
                    return administrationOfFacultyDto;
                }, splitOn: "PersonId, PassportId, AddressId").ToList();
            var dictionaryAdministrators = administrators
                .GroupBy(x => x.FacultyId)
                .ToDictionary(x => x.Key, x => x.Select(PoF => PoF.Administrator).ToList());
            foreach (var faculty in faculties)
            {
                faculty.AdministrationOfFaculty = dictionaryAdministrators.GetValueOrDefault(faculty.FacultyId);
            }
            return faculties;
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
                    string SqlQuery = @"DELETE FROM Faculty WHERE ID = @ID";
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

    public long Update(FacultyDto faculty)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                string SqlQuery;
                try
                {
                    SqlQuery = @"UPDATE Faculty SET NameFaculty = @NameFaculty, IdUniversity = @IdUniversity WHERE ID = @IdFaculty";
                    db.Execute(SqlQuery, faculty, transaction);
                    SqlQuery = @"DELETE FROM AdministrationOfFaculty WHERE IdFaculty = @IdFaculty";
                    db.Execute(SqlQuery, faculty, transaction);
                    var admin = faculty.IdAdministrators.Select(adminId => new
                    {
                        IdFaculty = faculty.IdFaculty,
                        IdAdministrators = adminId
                    }).ToList();
                    SqlQuery = @"INSERT INTO AdministrationOfFaculty (IdFaculty, IdAdministrator) VALUES (@IdFaculty, @IdAdministrators)";
                    db.Execute(SqlQuery, admin, transaction);
                    transaction.Commit();
                    return faculty.IdFaculty;
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

    public long? CheckNameFaculty(string nameFaculty, long universityId)
    {
        string SqlQuery = "SELECT ID FROM Faculty WHERE NameFaculty = @namefaculty AND IdUniversity = @universityId";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var check = db.Query<long?>(SqlQuery, new {  nameFaculty, universityId}).FirstOrDefault();
            check = check == 0 ? null : check;
            return check;
        }
    }
}