using Repository;
using Logger;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using UCore;
MyLogger logger = new ConsoleMyLogger();
DebugColumnOrder();
StudentRepository studentRepository = new StudentRepository(
    "Server=(localdb)\\MSSQLLocalDB;;Database=UniversityDB;Integrated Security=True", logger);
// var students = studentRepository.ReturnList(logger);
// foreach (var student in students)
// {
//     student.PrintInfo(logger);
// }

Student student2 = studentRepository.Get(1);
student2.PrintInfo(logger);
// Student student1 = new Student();
// student1.CountOfExamsPassed = 0;
// student1.Course = 1;
// student1.CreditScores = 0;
// student1.SkipHours = 0;
// student1.CriminalRecord = false;
// student1.MilitaryIdAvailability = IdMillitary.DidNotServe;
// student1.Passport = new Passport(2346, 111111, "St2", "St22", "St222", new DateTime(2007,11, 21),
//     new Address("Russia", "Moscow", "St1", 2), "1");
// studentRepository.Create(student1, logger);

static void DebugColumnOrder()
{
    using (IDbConnection db = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;;Database=UniversityDB;Integrated Security=True"))
    {
        var testQuery = @"
            SELECT 
        s._skipHours AS SkipHours,
        s._countOfExamsPassed AS CountOfExamsPassed, 
        s._creditScores AS CreditScores,
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
        a._houseNumber AS HouseNumber,
        ds._levelDegrees AS LevelDegrees,
        im._levelID AS LevelID
    FROM Student s
    INNER JOIN Passport p ON s._passportID = p.ID
    INNER JOIN Address a ON p._addressID = a.ID
    INNER JOIN DegreesStudy ds ON s._courseID = ds.ID
    INNER JOIN IdMillitary im ON s._millitaryID = im.ID";

        using (var reader = db.ExecuteReader(testQuery))
        {
            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine($"{i}: {reader.GetName(i)}");
                }
            }
            
        }
    }
}

