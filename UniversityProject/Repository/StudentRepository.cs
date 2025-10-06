
namespace Repository;
using UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Dapper.Json.Extensions;
using System.Reflection;
using System.Text.Json;
public class StudentRepository : IStudentRepository
{
    const string SQlQuerySelect = @"
    SELECT 
        s._skipHours AS SkipHours,
        s._countOfExamsPassed AS CountOfExamsPassed, 
        s._creditScores AS CreditScores,
        ds._levelDegrees AS LevelDegrees,
        im._levelID AS LevelID,
        p.ID as PassportID,
        p._serial AS Serial,
        p._number AS Number,
        p._firstName AS FirstName,
        p._lastName AS LastName,
        p._middleName AS MiddleName,
        p._birthDate AS BirthDate,
        a.ID as AddressID,
        a._country AS Country,
        a._city AS City,
        a._street AS Street,
        a._houseNumber AS HouseNumber
    FROM Student s
    INNER JOIN Passport p ON s._passportID = p.ID
    INNER JOIN Address a ON p._addressID = a.ID
    INNER JOIN DegreesStudy ds ON s._courseID = ds.ID
    INNER JOIN IdMillitary im ON s._millitaryID = im.ID";
    public string ConvertToJson(List<Student> students)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true, 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };
        return JsonSerializer.Serialize(students, options);
    }
    string ConnectionString = null;
    public StudentRepository(IGetDBPath getDBPath)
    {
        ConnectionString = getDBPath.ReturnPath();
    }

    public StudentRepository(string path)
    {
        ConnectionString = path;
    }

    public void Create(Student student, MyLogger myLogger)
    {
        try
        {

        }
        catch (SqlException ex)
        {
            myLogger.Error("SQL error " + ex.Message + ".Error number " + ex.Number);
        }
        catch (Exception ex)
        {
            myLogger.Error("General Error" + ex.Message);
        }
    using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var countOfExamsPassed = student.CountOfExamsPassed;
            var course = student.Course;
            var creditScores = student.CreditScores;
            var skipHours = student.SkipHours;
            var criminalRecord = student.CriminalRecord;
            IdMillitary millitaryIdAvailability = student.MilitaryIdAvailability;
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
            var birthData =  passport.BirthData/*.ToString().Remove(10, 8)*/;
            var placeReceipt = passport.PlaceReceipt;
            var sqlQuery = @"
    INSERT INTO Address 
    VALUES(@country, @city, @street, @placeReceipt)
    INSERT INTO Passport
       VALUES(@serial, 
           @firstName,
           @lastName, 
           @middleName, 
           @birthData, 
           (SELECT SCOPE_IDENTITY()), 
           @placeReceipt,
           @number); 
    INSERT INTO Student
        VALUES((SELECT SCOPE_IDENTITY()),
            (SELECT ID FROM IdMillitary WHERE _levelID = @millitaryIdAvailability),
            @criminalRecord,
            @course,
            @skipHours,
            @countOfExamsPassed, 
            @creditScores, 
            NULL);";
            db.Execute(sqlQuery, new
            {
                country = country, city = city, street = street, houseNumber = houseNumber, 
                serial = serial, number = number,  firstName = firstName, lastName = lastName, middleName = middleName,
                birthData = birthData,  placeReceipt = placeReceipt, millitaryIdAvailability = millitaryIdAvailability, 
                criminalRecord = criminalRecord, course = course, creditScores = creditScores, countOfExamsPassed = countOfExamsPassed,
                skipHours = skipHours
            });
        }
    }

    public void PrintAll(MyLogger myLogger)
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

    public List<Student> ReturnList(MyLogger myLogger)
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
    
    public string ReturnJson(MyLogger myLogger)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = @"SQlQuerySelect";
            var students = db.Query<Student, Passport, Address, Student>(sqlQuery,
                (student, passport, address) =>
                {
                    passport.Address = address;
                    student.Passport = passport;
                    return student;
                },
                splitOn: "PassportID, AddressID"
            ).ToList();
            return ConvertToJson(students);
        }
    }
    
    public Student Get(int id, MyLogger logger)
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
            logger.Info($"Возвращение студента - {student.Passport.FirstName}, {student.Passport.LastName}");
            return student;
        }
    }
    
    public void Update(Student student, MyLogger myLogger)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = "";
            db.Execute(sqlQuery, student);
        }
    }

    public void Delete(int ID, MyLogger myLogger)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = "DELETE FROM Student where ID = @ID;";
            db.Execute(sqlQuery, new{ID});
        }
    }

    
    private static List<Student> _student = new List<Student>(0);
}