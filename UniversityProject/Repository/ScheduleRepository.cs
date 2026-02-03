using  UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;
namespace Repository;

public class ScheduleRepository : IScheduleRepository
{
    private string _connectionString;
    private MyLogger _logger;
    const string SqlQuery = @"SELECT sc.Id, sc.DataWeekForScheduleId, dw.DaysOfWeek AS DataWeek, 
       sc.DataСoupleForScheduleId, dc.StartCouple, dc.EndCouple, sc.DirectionId, dr.DegreesStudyId as NumberOfCourse, dr.NameDirection, 
       dr.ChatId, dp.Id as DepartmentId, dp.NameDepartment, fc.ID AS FacultyId, 
       fc.NameFaculty, fc.IdUniversity AS UniversityId, un.Budget, un.NameUniversity ,
       sc.DisciplineId, ds.NameDiscipline, sc.TeacherId,tc.Id AS PersonId, tc.Salary,
       tc.CriminalRecord, im.LevelId AS MilitaryIdAvailability, p.ID AS PassportID,
       p.Serial,p.Number, p.FirstName, p.LastName, p.MiddleName, p.BirthData,
       a.ID AS AddressID, a.Country, a.City, a.Street, a.HouseNumber
    FROM Schedule sc
    JOIN Direction dr ON dr.Id = sc.DirectionId
    JOIN Department dp ON dp.Id = dr.DepartmentId
    JOIN Faculty fc ON fc.Id = dp.FacultyId
    JOIN University un ON un.Id = fc.IdUniversity
    JOIN Discipline ds ON ds.Id = sc.DisciplineId
    JOIN Teacher tc ON tc.id = sc.TeacherId
    JOIN Passport p ON tc.PassportId = p.ID
    JOIN Address a ON p.AddressId = a.ID
    JOIN IdMilitary im ON tc.MilitaryId = im.ID
    JOIN DataWeekForSchedule dw ON dw.Id = sc.DataWeekForScheduleId
    JOIN DataСoupleForSchedule dc ON dc.Id = sc.DataСoupleForScheduleId";
    public ScheduleRepository(IGetConnectionString getConnectionString, MyLogger logger)
    {
        _connectionString = getConnectionString.ReturnConnectionString();
        _logger = logger;
    }
    public long Create(ScheduleDto schedule)
    {
        using(IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    var sqlQuery = @"
    INSERT INTO Schedule (DirectionId, DisciplineId, TeacherId, DataWeekForScheduleId, DataСoupleForScheduleId)
    OUTPUT INSERTED.ID
    VALUES (@DirectionId, @DisciplineId, @TeacherId, 
            (SELECT ID FROM DataWeekForSchedule WHERE DaysOfWeek = @DataWeek),
            (SELECT ID FROM DataСoupleForSchedule WHERE StartCouple = @StartCouple AND EndCouple = @EndCouple))";
                    long id = db.QuerySingle<long>(sqlQuery, new
                    {
                        schedule.DirectionId, schedule.DisciplineId, schedule.TeacherId,
                        DataWeek = schedule.DataWeek.ToString(), StartCouple = schedule.StartCouple, 
                        EndCouple = schedule.EndCouple
                    }, transaction);
                    transaction.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }

    public Schedule Get(long Id)
    {
        using(IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            Schedule schedule  = db.Query<Schedule, Direction, Department, Faculty, University, Schedule>(
                SqlQuery + " Where sc.Id = @Id", (schedule, direction, department, faculty, universtity) => 
                {
                    faculty.University = universtity;
                    department.Faculty = faculty;
                    direction.Department = department;
                    schedule.Direction = direction;
                    return schedule;
                }, new { Id }, splitOn: "Id, DirectionId,DepartmentId,FacultyId,UniversityId").First();
            Discipline discipline = db.Query<Discipline>(SqlQuery + " Where sc.Id = @Id", new { Id }).First();
            Teacher teacher = db.Query<Teacher, Passport, Address, Teacher>(
                SqlQuery + " WHERE sc.ID = @ID",
                (teacher, Passport, Address) =>
                {
                    Passport.Address = Address;
                    teacher.Passport = Passport;
                    return teacher;
                }, new { ID = Id },
                splitOn: "PassportId, AddressId").First();
            schedule.Discipline = discipline;
            schedule.Teacher = teacher;
            return schedule;
        }
    }

    public List<Schedule> ReturnList()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            List<Schedule> schedules = db.Query<Schedule, Direction, Department, Faculty, University, Discipline, Schedule>(
                SqlQuery, (schedule, direction, department, faculty, universtity, discipline) =>
                {
                    faculty.University = universtity;
                    department.Faculty = faculty;
                    direction.Department = department;
                    schedule.Direction = direction;
                    schedule.Discipline = discipline;
                    return schedule;
                }, splitOn: "DirectionId,DepartmentId,FacultyId,UniversityId, DisciplineId").AsList();
            List<TeacherOfScheduleDTO> teachers = db.Query<TeacherOfScheduleDTO, Teacher, Passport, Address, TeacherOfScheduleDTO>(
                SqlQuery,
                (TeacherDTO, teacher, passport, Address) =>
                {
                    passport.Address = Address;
                    teacher.Passport = passport;
                    TeacherDTO.Teacher = teacher;
                    return TeacherDTO;
                }, splitOn: "TeacherId, PassportId, AddressId").ToList();
            var TeachersDir = teachers.GroupBy(x => x.Id)
                .ToDictionary(x => x.Key, x => x.Select(teachers => teachers.Teacher).First());
            foreach (var schedule in schedules)
            {
                schedule.Teacher = TeachersDir.GetValueOrDefault(schedule.Id);
            }
            return schedules;
        }
    }
    public List<Schedule> ReturnListForDirectionId(long dirId)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            List<Schedule> schedules = db.Query<Schedule, Direction, Department, Faculty, University, Discipline, Schedule>(
                SqlQuery + " WHERE dr.Id = @dirId", (schedule, direction, department, faculty, universtity, discipline) =>
                {
                    faculty.University = universtity;
                    department.Faculty = faculty;
                    direction.Department = department;
                    schedule.Direction = direction;
                    schedule.Discipline = discipline;
                    return schedule;
                }, new {dirId} , splitOn: "DirectionId,DepartmentId,FacultyId,UniversityId, DisciplineId").AsList();
            List<TeacherOfScheduleDTO> teachers = db.Query<TeacherOfScheduleDTO, Teacher, Passport, Address, TeacherOfScheduleDTO>(
                SqlQuery + " WHERE dr.Id = @dirId",
                (TeacherDTO, teacher, passport, Address) =>
                {
                    passport.Address = Address;
                    teacher.Passport = passport;
                    TeacherDTO.Teacher = teacher;
                    return TeacherDTO;
                }, new { dirId }, splitOn: "TeacherId, PassportId, AddressId").ToList();
            var TeachersDir = teachers.GroupBy(x => x.Id)
                .ToDictionary(x => x.Key, x => x.Select(teachers => teachers.Teacher).First());
            foreach (var schedule in schedules)
            {
                schedule.Teacher = TeachersDir.GetValueOrDefault(schedule.Id);
            }
            return schedules;
        }
    }
    public void Delete(long ID)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sqlQuery = "DELETE FROM Schedule where ID = @ID;";
            db.Execute(sqlQuery, new{ID});
        }
    }

    public long Update(ScheduleDto schedule)
    {
        using(IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    var sqlQuery = @"
    UPDATE Schedule SET DirectionId = @DirectionId, DisciplineId = @DisciplineId, TeacherId = @TeacherId,
                        DataWeekForScheduleId = (SELECT ID FROM DataWeekForSchedule WHERE DaysOfWeek = @DataWeek),
                        DataСoupleForScheduleId = (SELECT ID FROM DataСoupleForSchedule WHERE StartCouple = @StartCouple AND EndCouple = @EndCouple)
                        WHERE ID = @iId";
                    db.Execute(sqlQuery, new
                    {
                        schedule.DirectionId, schedule.DisciplineId, schedule.TeacherId,
                        DataWeek = schedule.DataWeek.ToString(), StartCouple = schedule.StartCouple, 
                        EndCouple = schedule.EndCouple, schedule.Id
                    }, transaction);
                    transaction.Commit();
                    return schedule.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }
}
