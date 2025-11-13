
namespace Repository;
using UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;
public class StudentRepository : IStudentRepository
{
    const string SQlQuerySelect = @"
    SELECT 
        s.Id AS PersonId,
        s.SkipHours,
        s.CountOfExamsPassed, 
        s.CreditScores,
        ds.LevelDegrees,
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
    FROM Student s
    INNER JOIN Passport p ON s.PassportId = p.ID
    INNER JOIN Address a ON p.AddressId = a.ID
    INNER JOIN DegreesStudy ds ON s.CourseId = ds.ID
    INNER JOIN IdMilitary im ON s.MilitaryId = im.ID";
    
    public StudentRepository(IGetConnectionString getConnectionString, MyLogger logger)
    {
        ConnectionString = getConnectionString.ReturnConnectionString();
        myLogger = logger;
    }

    public StudentRepository(string connectionString, MyLogger logger)
    {
        ConnectionString = connectionString;
        myLogger = logger;
    }

    public int Create(Student student)
    {
        Passport passport = student.Passport;
        Address address = passport.Address;
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            db.Open();
            using(IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
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
                INSERT INTO Student(PassportId, MilitaryId, CriminalRecord, CourseId, SkipHours, CountOfExamsPassed, CreditScores)
                    VALUES((SELECT MAX(ID) FROM PASSPORT),
                        @MilitaryIdAvailability + 1,
                        @CriminalRecord,
                        @Course,
                        @SkipHours,
                        @CountOfExamsPassed, 
                        @CreditScores)";
                        db.Execute(sqlQuery, student
                            , transaction);
                        transaction.Commit();
                        var id = db.QueryFirstOrDefault<int>("SELECT MAX(ID) FROM Student");
                        return id;
                    }
                    catch(Exception ex)
                    {
                        myLogger.Error("An error occured during transaction" + ex.Message);
                        transaction.Rollback();
                        throw;
                    }
                }
        }
        
    }

    public void PrintAll()
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = SQlQuerySelect;
            List <Student> students = db.Query<Student, Passport, Address, Student>(sqlQuery,
                (student, passport, address) =>
                {
                    passport.Address = address;
                    student.Passport = passport;
                    return student;
                },
                splitOn: "PassportID, AddressID"
            ).ToList();
            foreach (var student in students)
            {
                student.PrintInfo(myLogger);
            }
        }
    }

    public List<Student> ReturnList()
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = SQlQuerySelect;
            return db.Query<Student, Passport, Address, Student>(sqlQuery,
                (student, passport, address) =>
                {
                    passport.Address = address;
                    student.Passport = passport;
                    return student;
                },
                splitOn: "PassportID, AddressID"
            ).ToList();
        }
    }   
    
    public Student Get(int id)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = SQlQuerySelect + " WHERE s.ID = @id";
            var student = db.Query<Student, Passport, Address, Student>(sqlQuery,
                (student, passport, address) =>
                {
                    passport.Address = address;
                    student.Passport = passport;
                    return student;
                },
                (new { id = id }),
                splitOn: "PassportID, AddressID"
            ).FirstOrDefault();
            myLogger.Info($"Возвращение студента - {student.Passport.FirstName}, {student.Passport.LastName}");
            return student;
        }
    }
    
    public int Update(Student student)
    {
        Passport passport = student.Passport;
        Address address = passport.Address;
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                { 
                    string sqlQuery = @"SELECT PassportID FROM Student WHERE Id = @PersonID";
                    passport.PassportId = db.Query<int>(sqlQuery, student, transaction).First();
                    sqlQuery = @"SELECT AddressId FROM Passport WHERE Id = @PassportID";
                    address.AddressId = db.Query<int>(sqlQuery, passport, transaction).First();
                    sqlQuery = @"UPDATE Address 
                SET Country = @Country,  City = @City, Street = @Street, HouseNumber = @HouseNumber
                WHERE ID = @AddressId";
                    db.Execute(sqlQuery, address, transaction);
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
                    sqlQuery = @"
                    UPDATE STUDENT 
                    SET MilitaryId = @MilitaryIdAvailability + 1, 
                        CriminalRecord = @CriminalRecord, 
                        CourseId = @Course, 
                        SkipHours = @SkipHours,
                        CountOfExamsPassed = @CountOfExamsPassed, 
                        CreditScores = @CreditScores
                    WHERE ID = @PersonId";
                    db.Execute(sqlQuery, student, transaction);
                    transaction.Commit();
                    return student.PersonId;
                }
                catch (Exception ex)
                {
                    myLogger.Error("An error occured during transaction" + ex.Message);
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    public void Delete(int ID)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = "DELETE FROM Student where ID = @ID;";
            db.Execute(sqlQuery, new{ID});
        }
    }
    
    public void DeleteAddress(int ID)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = "DELETE FROM Address where ID = @ID;";
            db.Execute(sqlQuery, new{ID});
        }
    }
    
    public void DeletePassport(int ID)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = "DELETE FROM Passport where ID = @ID;";
            db.Execute(sqlQuery, new{ID});
        }
    }
    
    string ConnectionString = null;
    private MyLogger myLogger;
}