
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
                        myLogger.Info("Start Transaction");
                        var sqlQuery = @"
                INSERT INTO Address 
                VALUES(@address.country, @address.city, @address.street, @address.houseNumber)
                INSERT INTO Passport
                       VALUES(@passport.serial,
                           @passport.number,
                           @passport.firstName,
                           @passport.lastName, 
                           @passport.middleName, 
                           @passport.birthData, 
                           (SELECT MAX(ID) FROM ADDRESS), 
                           @passport.placeReceipt)
                INSERT INTO Student
                    VALUES((SELECT MAX(ID) FROM PASSPORT),
                        @student.militaryIdAvailability + 1,
                        @student.criminalRecord,
                        @student.course,
                        @student.skipHours,
                        @student.countOfExamsPassed, 
                        @student.creditScores)";
                        db.Execute(sqlQuery, new
                        {
                            address, passport, student
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
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    var sqlQuery = @"UPDATE Address 
                SET Country = @country,  City = @city, Street = @street, HouseNumber = @houseNumber
                WHERE ID = (SELECT AddressId FROM PASSPORT WHERE ID = (SELECT PassportId FROM STUDENT WHERE ID = @PersonId))";
                    db.Execute(sqlQuery, new{student.Passport.Address, student}, transaction);
                    sqlQuery = @"UPDATE Passport 
                SET Serial = @serial, Number = @number,  FirstName = @firstName, LastName = @lastName, MiddleName = @middleName, 
                    BirthData = @birthData, PlaceReceipt = @placeReceipt
                WHERE ID = (SELECT PassportId FROM STUDENT WHERE ID = @PersonId)";
                    db.Execute(sqlQuery, new
                    {
                        student.Passport, student
                    }, transaction);
                    sqlQuery = @"UPDATE STUDENT 
                SET MilitaryId = @militaryIdAvailability, CriminalRecord = @criminalRecord, CourseId = @course, SkipHours = @skipHours,
                    CountOfExamsPassed = @countOfExamsPassed, CreditScores = @creditScores
                WHERE ID = @PersonId";
                    db.Execute(sqlQuery, new
                    {
                         student
                    }, transaction);
                    myLogger.Info("Transaction complete");
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