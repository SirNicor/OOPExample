namespace Repository;
using UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;
public class WorkerTeacherRepository : IWorkerTeacherRepository
{
    private string SqlQuerySelect = @"
SELECT 
        tc.Id AS PersonId,
        tc.Salary,
        tc.CriminalRecord,
        im.LevelId AS MilitaryIdAvailability,
        p.ID AS PassportID,
        p.Serial,
        p.Number,
        p.FirstName,
        p.LastName,
        p.MiddleName,
        p.BirthData,
        a.ID AS AddressID,
        a.Country,
        a.City,
        a.Street,
        a.HouseNumber
    FROM Teacher tc
    INNER JOIN Passport p ON tc.PassportId = p.ID
    INNER JOIN Address a ON p.AddressId = a.ID
    INNER JOIN IdMilitary im ON tc.MilitaryId = im.ID";
    private MyLogger _logger;
    private string _connectionString;
    public WorkerTeacherRepository(IGetConnectionString getConnectionString, MyLogger logger)
    {
        _connectionString = getConnectionString.ReturnConnectionString();
        _logger = logger;
    }
    public Teacher Get(long Id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var teacher = db.Query<Teacher, Passport, Address, Teacher>(
                SqlQuerySelect + " WHERE tc.ID = @ID",
                (teacher, Passport, Address) =>
                {
                    Passport.Address = Address;
                    teacher.Passport = Passport;
                    return teacher;
                }, new{ ID = Id},
                splitOn: "PassportId, AddressId").FirstOrDefault();
            return teacher;
        }
    }
    public void PrintAll()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var teachers = db.Query<Teacher, Passport, Address, Teacher>(
                SqlQuerySelect,
                (teacher, Passport, Address) =>
                {
                    Passport.Address = Address;
                    teacher.Passport = Passport;
                    return teacher;
                },
                splitOn: "PassportId, AddressId").ToList();
            foreach (var teacher in teachers)
            {
                teacher.PrintInfo(_logger);
            }
        }
    }
    public long Create(Teacher teacher)
    {
        var passport = teacher.Passport;
        var address = passport.Address;
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using(IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        _logger.Info("Start Transaction");
                        var sqlQuery = @"
                INSERT INTO Address(Country, City, Street, HouseNumber)
                VALUES(@Country, @City, @Street, @HouseNumber)";
                        db.Execute(sqlQuery, address, transaction);
                        sqlQuery = @"
                INSERT INTO Passport(Serial, Number, FirstName, LastName, MiddleName, BirthData, AddressId, PlaceReceipt)
                       VALUES(@Serial,
                           @Number,
                           @FirstName,
                           @LastName, 
                           @MiddleName, 
                           @BirthData, 
                           (SELECT MAX(ID) FROM ADDRESS), 
                           @PlaceReceipt)";
                        db.Execute(sqlQuery, passport, transaction);
                        sqlQuery = @"
                INSERT INTO Teacher(Salary, CriminalRecord, PassportId, MilitaryId)
                    VALUES(@Salary,
                        @CriminalRecord,
                        (SELECT MAX(ID) FROM PASSPORT),
                        @MilitaryIdAvailability + 1)
                        ";
                        db.Execute(sqlQuery, teacher, transaction);
                        transaction.Commit();
                        var id = db.QueryFirstOrDefault<int>("SELECT MAX(ID) FROM Teacher");
                        return id;
                    }
                    catch(Exception ex)
                    {
                        _logger.Error("An error occured during transaction" + ex.Message);
                        transaction.Rollback();
                        throw;
                    }
                }
        }
    }
    public List<Teacher> ReturnList()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var teachers = db.Query<Teacher, Passport, Address, Teacher>(
                SqlQuerySelect,
                (teacher, Passport, Address) =>
                {
                    Passport.Address = Address;
                    teacher.Passport = Passport;
                    return teacher;
                },
                splitOn: "PassportId, AddressId").ToList();
            return teachers;
        }
    }
    public void Delete(long Id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Execute("DELETE FROM Teacher WHERE ID = @ID", new { ID = Id});
            _logger.Info("Delete administrator - " + Id);
        }
    }
    public long Update(Teacher teacher)
    {
        var passport = teacher.Passport;
        var address = passport.Address;
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    string sqlQuery = @"SELECT PassportID FROM Teacher WHERE Id = @PersonID";
                    passport.PassportId = db.Query<int>(sqlQuery, teacher, transaction).First();
                    sqlQuery = @"SELECT AddressId FROM Passport WHERE Id = @PassportID";
                    address.AddressId = db.Query<int>(sqlQuery, passport, transaction).First();
                    sqlQuery = @"UPDATE Address 
                SET Country = @Country,  City = @City, Street = @Street, HouseNumber = @HouseNumber
                WHERE ID = @AddressId";
                    db.Execute(sqlQuery, address , transaction);
                    sqlQuery = @"
                    UPDATE PASSPORT 
                    SET Serial = @Serial, 
                        Number = @Number,  
                        FirstName = @FirstName, 
                        LastName = @LastName, 
                        MiddleName = @MiddleName, 
                        BirthData = @BirthData, 
                        PlaceReceipt = @PlaceReceipt
                    WHERE ID = @PassportID";
                    db.Execute(sqlQuery, passport, transaction);
                    sqlQuery = @"UPDATE Teacher
                    SET Salary = @Salary, MilitaryId = @MilitaryIdAvailability, CriminalRecord = @CriminalRecord
                    WHERE ID = @PersonId";
                    db.Execute(sqlQuery, teacher, transaction);
                    transaction.Commit();
                    return teacher.PersonId;
                }
                catch(Exception ex)
                {
                    _logger.Error("An error occured during transaction" + ex.Message);
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}