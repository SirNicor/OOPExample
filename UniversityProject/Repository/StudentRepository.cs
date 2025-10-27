
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
    
    string ConnectionString = null;
    private MyLogger myLogger;
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

    public void Create(Student student)
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
            string millitaryIdAvailability = student.MilitaryIdAvailability.ToString();
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
        VALUES(@country, @city, @street, @houseNumber)";
            try
            {
                db.Execute(sqlQuery, new{country, city, street, houseNumber});
                myLogger.Info("Added Address");
                try
                {
                    sqlQuery = @"
            INSERT INTO Passport
               VALUES(@serial, 
                   @firstName,
                   @lastName, 
                   @middleName, 
                   @birthData, 
                   (SELECT MAX(ID) FROM ADDRESS), 
                   @placeReceipt,
                   @number);";
                    db.Execute(sqlQuery,
                        new { serial, firstName, lastName, middleName, birthData, placeReceipt, number });
                    myLogger.Info("Added Passport");
                    try
                    {
                        sqlQuery = @"INSERT INTO Student
            VALUES((SELECT MAX(ID) FROM PASSPORT),
                (SELECT ID FROM IdMillitary WHERE _levelID = @millitaryIdAvailability),
                @criminalRecord,
                @course,
                @skipHours,
                @countOfExamsPassed, 
                @creditScores, 
                NULL);";
                        db.Execute(sqlQuery,
                            new
                            {
                                millitaryIdAvailability, criminalRecord, course, skipHours, countOfExamsPassed,
                                creditScores
                            });
                        myLogger.Info("Added Student");
                    }
                    catch (SqlException ex)
                    {
                        sqlQuery = "DELETE FROM Address WHERE ID = (SELECT MAX(ID) FROM ADDRESS)";
                        db.Execute(sqlQuery);
                        sqlQuery = "DELETE FROM Passport WHERE ID = (SELECT MAX(ID) FROM PASSPORT)";
                        db.Execute(sqlQuery);
                        myLogger.Info("Deleted Address and Passport. Not added Student");
                    }
                }
                catch(SqlException ex)
                {
                    sqlQuery = "DELETE FROM Address WHERE ID = (SELECT MAX(ID) FROM ADDRESS)";
                    db.Execute(sqlQuery);
                    myLogger.Info("Deleted Address and not added Passport" + ex.Message);
                }
            }
            catch(SqlException ex)
            {
                myLogger.Info("not added Address " + ex.Message);
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
}