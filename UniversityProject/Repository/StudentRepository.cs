
namespace Repository;
using UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using static Dapper.SqlBuilder;

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

    public long Create(StudentDtoForPage student)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            db.Open();
            using(IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        var sqlQuery = @"
                INSERT INTO Address(Country, City, Street, HouseNumber)
                VALUES(@country, @city, @state, @houseNumber)
                SELECT SCOPE_IDENTITY()";
                        student.addressId = db.Query<long>(sqlQuery, student, transaction).First();
                        sqlQuery = @"
                INSERT INTO Passport(Serial, Number, FirstName, LastName, MiddleName, BirthData, AddressId, PlaceReceipt)
                       VALUES(@serial,
                           @number,
                           @firstName,
                           @lastName, 
                           @middleName, 
                           @dob, 
                           @addressId, 
                           @PlaceReceipt)
                           SELECT SCOPE_IDENTITY()";
                        student.passportId = db.Query<long>(sqlQuery, student, transaction).First();
                        sqlQuery = @"
                INSERT INTO Student(PassportId, MilitaryId, CriminalRecord, CourseId, SkipHours, CountOfExamsPassed, CreditScores)
                    VALUES(@passportId,
                        @MilitaryIdAvailability + 1,
                        @CriminalRecord,
                        @Course,
                        @SkipHours,
                        @CountOfExamsPassed, 
                        @CreditScores)
                        SELECT SCOPE_IDENTITY()";
                        student.studentId = db.Query<long>(sqlQuery, student
                            , transaction).First();
                        transaction.Commit();
                        return student.passportId;
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
    
    public Student Get(long id)
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

    public StudentDtoForPage GetStudentPage(long studentId)
    {
        const string SQlQuerySelect = @"
    SELECT 
        s.Id AS studentId,
        s.SkipHours,
        s.CountOfExamsPassed, 
        s.CreditScores,
        s.CriminalRecord,
        s.CourseID as course,
        ds.LevelDegrees,
        im.LevelId AS MilitaryIdAvailability,
        p.ID AS passportID,
        p.Serial,
        p.Number,
        p.placeReceipt,
        p.FirstName,
        p.LastName,
        p.MiddleName,
        p.BirthData as dob,
        a.ID AS addressID,
        a.Country,
        a.City,
        a.Street as state,
        a.HouseNumber
    FROM Student s
    INNER JOIN Passport p ON s.PassportId = p.ID
    INNER JOIN Address a ON p.AddressId = a.ID
    INNER JOIN DegreesStudy ds ON s.CourseId = ds.ID
    INNER JOIN IdMilitary im ON s.MilitaryId = im.ID
    WHERE s.Id = @studentId;";
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            StudentDtoForPage student = db.QueryFirst<StudentDtoForPage>(SQlQuerySelect, new {studentId});
            return student;
        }
    }

    public Tuple<List<StudentTableDTO>, long> GetStudentTableDTO(long FirstId, long countOfRow, string? SortColumn, string? SortOrder, 
        FilterDto? filter)
    {
        var builder = new SqlBuilder();
        SortOrder = SortOrder ?? "ASC";
        SortColumn = SortColumn ?? "s.Id";
        SortOrder = SortOrder == "null"? "ASC" : SortOrder;
        SortColumn = SortColumn == "null" ? "s.Id" : SortColumn;
        myLogger.Info($"GetStudentTableDto: FirstId:{FirstId},  count:{countOfRow}, sortColumn:{SortColumn}, sortOrder:{SortOrder}," +
                      $"filterCourse:{filter.FilterCourse}, BitrhDay: {filter.FilterBirthDayStart} {filter.FilterBirthDayEnd}," +
                      $"filterSkipHours: {filter.FilterSkipHoursStart} {filter.FilterSkipHoursEnd}, filtertotalScore: {filter.FilterTotalScore}");
        string Sql = $@"SELECT 
        s.Id AS studentId,
        s.SkipHours,
        s.CountOfExamsPassed, 
        s.CreditScores,
        IIF(s.CountOfExamsPassed = 0, 0, CAST(s.CreditScores AS DECIMAL(18, 1)) / s.CountOfExamsPassed) AS TotalScore,
        s.CourseId as Course,
        im.LevelId AS MilitaryIdAvailability,
        p.ID AS PassportID,
        p.Serial,
        p.Number,
        CONCAT_WS(' ',p.FirstName, p.LastName, p.MiddleName) AS Fio,
        p.BirthData as Dob,
        a.ID AS AddressID,
        CONCAT_WS(' ',a.Country,a.City, a.Street, a.HouseNumber) AS Address
    FROM Student s
    INNER JOIN Passport p ON s.PassportId = p.ID
    INNER JOIN Address a ON p.AddressId = a.ID
    INNER JOIN DegreesStudy ds ON s.CourseId = ds.ID
    INNER JOIN IdMilitary im ON s.MilitaryId = im.ID
    /**where**/
    ORDER BY {SortColumn} {SortOrder}
    OFFSET @FirstId ROWS FETCH NEXT @countOfRow ROWS ONLY";
        var template = builder.AddTemplate(Sql, new
        {
            FirstId, countOfRow, filter.FilterBirthDayStart, filter.FilterCourse,
            filter.FilterSkipHoursStart, filter.FilterSkipHoursEnd, filter.FilterTotalScore,
            filter.FilterBirthDayEnd
        });
        Sql = $@"SELECT COUNT(*)
    FROM Student s
    INNER JOIN Passport p ON s.PassportId = p.ID
    INNER JOIN Address a ON p.AddressId = a.ID
    INNER JOIN DegreesStudy ds ON s.CourseId = ds.ID
    INNER JOIN IdMilitary im ON s.MilitaryId = im.ID
    /**where**/";
        if (filter.FilterCourse is not null)
        {
            long numberOfCourse = (long)filter.FilterCourse;
            builder.Where($"s.CourseId = {numberOfCourse}");
        }

        if (filter.FilterBirthDayStart is not null && filter.FilterBirthDayEnd is not null)
        {
            builder.Where("p.BirthData >= @FilterBirthDayStart AND p.BirthData <= @FilterBirthDayEnd");
        }

        if (filter.FilterSkipHoursEnd is not null && filter.FilterSkipHoursStart is not null)
        {
            builder.Where("s.SkipHours >= @FilterSkipHoursStart and  s.SkipHours <= @FilterSkipHoursEnd");
        }

        if (filter.FilterTotalScore is not null)
        {
            
        }
        var templateOfPage = builder.AddTemplate(Sql, new { FirstId, countOfRow, filter.FilterBirthDayStart, filter.FilterCourse,
            filter.FilterSkipHoursStart, filter.FilterSkipHoursEnd, filter.FilterTotalScore,
            filter.FilterBirthDayEnd});
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var students = db.Query<StudentTableDTO>(template.RawSql, template.Parameters).ToList();
            var allCount = db.Query<long>(templateOfPage.RawSql, template.Parameters).First();
            return new Tuple<List<StudentTableDTO>, long>(students, allCount);
        }
    }

    public long GetCount()
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            return db.Query<long>("SELECT COUNT(*) FROM Student").First();
        }
    }

    public Student? GetStudentForChatId(string chatId)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = SQlQuerySelect + " WHERE s.ChatId = @chatId";
            var student = db.Query<Student, Passport, Address, Student>(sqlQuery,
                (student, passport, address) =>
                {
                    passport.Address = address;
                    student.Passport = passport;
                    return student;
                },
                (new { chatId }),
                splitOn: "PassportID, AddressID"
            ).FirstOrDefault();
            return student;
        }
    }

    public long? CheckName(string firstName, string lastName)
    {
        string SqlQuery = @"SELECT s.ID FROM Student S 
    INNER JOIN Passport p ON s.PassportId = p.ID
    WHERE p.FirstName = @firstName AND p.LastName = @lastName";
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var check = db.Query<long?>(SqlQuery, new {  firstName, lastName }).FirstOrDefault();
            check = check == 0 ? null : check;
            return check;
        }
    }
    public long? Update(StudentDtoForPage student)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                { 
                    string sqlQuery = @"UPDATE Address 
                SET Country = @country,  City = @city, Street = @state, HouseNumber = @houseNumber
                WHERE Id = @addressId";
                    db.Execute(sqlQuery, student, transaction);
                    sqlQuery = @"
                    UPDATE PASSPORT 
                    SET Serial = @serial, 
                        Number = @number,  
                        FirstName = @firstName, 
                        LastName = @lastName, 
                        MiddleName = @middleName, 
                        BirthData = @dob, 
                        PlaceReceipt = @placeReceipt
                    WHERE Id = @passportId";
                    db.Execute(sqlQuery, student, transaction);
                    sqlQuery = @"
                    UPDATE STUDENT 
                    SET
                        CriminalRecord = @criminalRecord, 
                        CourseId = @course, 
                        SkipHours = @skipHours,
                        CountOfExamsPassed = @countOfExamsPassed, 
                        CreditScores = @creditScores
                    WHERE ID = @studentId";
                    db.Execute(sqlQuery, student, transaction);
                    transaction.Commit();
                    return student.studentId;
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

    public void Delete(long ID)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = "DELETE FROM Student where ID = @ID;";
            db.Execute(sqlQuery, new{ID});
        }
    }
    
    public void DeleteAddress(long ID)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var sqlQuery = "DELETE FROM Address where ID = @ID;";
            db.Execute(sqlQuery, new{ID});
        }
    }
    
    public void DeletePassport(long ID)
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