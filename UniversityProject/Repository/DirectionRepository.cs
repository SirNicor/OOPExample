namespace Repository;
using UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;
public class DirectionRepository : IDirectionRepository
{
    private string _connectionString;
    private MyLogger _logger;

    private const string SqlSelectDirectionQuery =
        @"SELECT dr.Id AS DirectionId, dr.DegreesStudyId as NumberOfCourse, dr.NameDirection, 
       dr.ChatId, dp.Id as DepartmentId, dp.NameDepartment, fc.ID AS FacultyId, 
       fc.NameFaculty, fc.IdUniversity AS UniversityId, un.Budget, un.NameUniversity FROM Direction dr
JOIN Department dp ON dp.Id = dr.DepartmentId
JOIN Faculty fc ON fc.Id = dp.FacultyId
JOIN University un ON un.Id = fc.IdUniversity ";
    private const string SqlSelectStudentOfDirectionQuery =
        @"SELECT SoD.DirectionId, s.Id AS PersonId,s.SkipHours,s.CountOfExamsPassed, 
s.CreditScores,ds.LevelDegrees,im.LevelId AS MilitaryIdAvailability,p.ID AS PassportID,
p.Serial,p.Number,p.FirstName,p.LastName,p.MiddleName,p.BirthData,
a.ID AS AddressID, a.Country,a.City,a.Street,a.HouseNumber 
FROM StudentOfDirection SoD
INNER JOIN Student s ON s.Id = SoD.StudentId
INNER JOIN Passport p ON s.PassportId = p.ID
INNER JOIN Address a ON p.AddressId = a.ID
INNER JOIN DegreesStudy ds ON s.CourseId = ds.ID
INNER JOIN IdMilitary im ON s.MilitaryId = im.ID ";

    private const string SqlSelectDisciplineOfDirectionQuery =
        @"SELECT DoD.DirectionId, DoD.DisciplineId, ds.NameDiscipline 
FROM DisciplineOfDirection DoD
JOIN Discipline ds ON ds.Id = DoD.DisciplineId";
    public DirectionRepository(IGetConnectionString getConnectionString, MyLogger logger)
    {
        _connectionString = getConnectionString.ReturnConnectionString();
        _logger = logger;
    }
    public long Create(DirectionDto direction)
    {
        throw new NotImplementedException();
    }

    public Direction Get(long Id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            List<Student> students = db.Query<Student, Passport, Address, Student>(SqlSelectStudentOfDirectionQuery + "WHERE SoD.DirectionId = @Id",
                (student, passport, address) =>
                {
                    passport.Address = address;
                    student.Passport = passport;
                    return student;
                },
                new { Id = Id }, splitOn: "PersonId,PassportID,AddressID").ToList();
            List<Discipline> disciplines = db.Query<Discipline>(SqlSelectDisciplineOfDirectionQuery + " WHERE DoD.DirectionId = @Id", new { Id = Id }).ToList();
            Direction direction = db.Query<Direction, Department, Faculty, University, Direction>(SqlSelectDirectionQuery + "WHERE dr.Id = @Id",
                (direction, department, faculty, university) =>
                {
                    faculty.University = university;
                    department.Faculty = faculty;
                    direction.Department = department;
                    return direction;
                },
                new{Id}, splitOn: "DirectionId,DepartmentId,FacultyId,UniversityId").Single();
            direction.Students = students;
            direction.Disciplines = disciplines;
            return direction;
        }
    }

    public List<Direction> ReturnList()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            List<StudentOfDirectionDto> students = db.Query<StudentOfDirectionDto, Student, Passport, Address, StudentOfDirectionDto>(SqlSelectStudentOfDirectionQuery,
                (studentOfDirectionDto, student, passport, address) =>
                {
                    passport.Address = address;
                    student.Passport = passport;
                    studentOfDirectionDto.Student = student;
                    return studentOfDirectionDto;
                }, splitOn: "PersonId,PassportID,AddressID").ToList();
            List<DisciplineOfDirectionDto> disciplines = db.Query<DisciplineOfDirectionDto>(SqlSelectDisciplineOfDirectionQuery).ToList();
            List<Direction> directions = db.Query<Direction, Department, Faculty, University, Direction>(SqlSelectDirectionQuery,
                (direction, department, faculty, university) =>
                {
                    faculty.University = university;
                    department.Faculty = faculty;
                    direction.Department = department;
                    return direction;
                }, splitOn: "DirectionId,DepartmentId,FacultyId,UniversityId").ToList();
            var dirStudents = students.GroupBy(x => x.DirectionId)
                .ToDictionary(x => x.Key, x => x.Select(x1 => x1.Student).ToList());
            var dirDisciplines = disciplines.GroupBy(x => x.DirectionId)
                .ToDictionary(x => x.Key, x => x.Select(x1 => x1.Discipline).ToList());
            foreach (var direction in directions)
            {
                direction.Students = dirStudents.GetValueOrDefault(direction.DirectionId);
                direction.Disciplines = dirDisciplines.GetValueOrDefault(direction.DirectionId);
            }
            return directions;
        }
    }

    public void Delete(long ID)
    {
        throw new NotImplementedException();
    }

    public long Update(DirectionDto direction)
    {
        throw new NotImplementedException();
    }

    public long? CheckNameDirection(string nameDirection, long departmentId)
    {
        string SqlQuery = "SELECT ID FROM Direction WHERE NameDirection = @nameDirection AND DepartmentId = @departmentId";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var check = db.Query<long?>(SqlQuery, new {  nameDirection, departmentId }).FirstOrDefault();
            check = check == 0 ? null : check;
            return check;
        }
    }

    public bool AuthorizationVerification(long chatId)
    {
        string SqlQuery = "SELECT DirectionId FROM StudentOfDirection WHERE ChatId = @chatId";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var check = db.Query<long?>(SqlQuery, new { chatId }).FirstOrDefault();
            check = check == 0 ? null : check;
            if (check == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public bool CheckStudent(long StudentId)
    {
        string SqlQuery = "SELECT DirectionId FROM StudentOfDirection WHERE StudentId = @StudentId";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var check = db.Query<long?>(SqlQuery, new { StudentId }).FirstOrDefault();
            check = check == 0 ? null : check;
            if (check == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}