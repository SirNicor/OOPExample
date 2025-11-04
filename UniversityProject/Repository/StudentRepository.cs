
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
        s.SkipHours AS SkipHours,
        s.CountOfExamsPassed AS CountOfExamsPassed, 
        s.CreditScores AS CreditScores,
        ds.LevelDegrees AS LevelDegrees,
        im.LevelId AS MilitaryIdAvailability,
        p.ID as PassportID,
        p.Serial AS Serial,
        p.Number AS Number,
        p.FirstName AS FirstName,
        p.LastName AS LastName,
        p.MiddleName AS MiddleName,
        p.BirthData AS BirthDate,
        a.ID as AddressID,
        a.Country AS Country,
        a.City AS City,
        a.Street AS Street,
        a.HouseNumber AS HouseNumber
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
        var countOfExamsPassed = student.CountOfExamsPassed;
        var course = student.Course;
        var creditScores = student.CreditScores;
        var skipHours = student.SkipHours;
        var criminalRecord = student.CriminalRecord;
        var militaryIdAvailability = student.MilitaryIdAvailability;
        var passport = student.Passport;
        var address = passport.Address;
        var city =  address.City;
        var houseNumber = address.HouseNumber;
        var country = address.Country;
        var street = address.Street;
        var serial = passport.Serial;
        var number = passport.Number;
        var firstName = passport.FirstName;
        var lastName = passport.LastName;
        var middleName = passport.MiddleName;
        var birthData =  passport.BirthData;
        var placeReceipt = passport.PlaceReceipt;
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            db.Open();
            using(IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        myLogger.Info("Start Transaction");
                        var sqlQuery = @"
                INSERT INTO Address 
                VALUES(@country, @city, @street, @houseNumber)
                INSERT INTO Passport
                       VALUES(@serial,
                           @number,
                           @firstName,
                           @lastName, 
                           @middleName, 
                           @birthData, 
                           (SELECT MAX(ID) FROM ADDRESS), 
                           @placeReceipt)
                INSERT INTO Student
                    VALUES((SELECT MAX(ID) FROM PASSPORT),
                        @militaryIdAvailability + 1,
                        @criminalRecord,
                        @course,
                        @skipHours,
                        @countOfExamsPassed, 
                        @creditScores)";
                        db.Execute(sqlQuery, new
                        {
                            country, city, street, houseNumber, serial, number,firstName, lastName, middleName, birthData,placeReceipt, militaryIdAvailability,
                            criminalRecord, course, skipHours,  countOfExamsPassed, creditScores
                        }, transaction);
                        transaction.Commit();
                        myLogger.Info("End Transaction");
                        var id = db.QueryFirstOrDefault<int>("SELECT MAX(ID) FROM Student");
                        return id;
                    }
                    catch(Exception ex)
                    {
                        myLogger.Error("An error occured during transaction" + ex.Message);
                        transaction.Rollback();
                        return -1;
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
                Console.WriteLine(student.Passport.FirstName);
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
    
    public void Update(Student student)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = "";
            db.Execute(sqlQuery, student);
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