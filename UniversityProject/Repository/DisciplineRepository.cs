namespace Repository;
using UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;
public class DisciplineRepository : IDisciplineRepository
{
    private const string _sqlSelectDisciplineQuery = @"SELECT Id AS DisciplineId, NameDiscipline FROM Discipline ";

    private const string _sqlSelectTacherOfDisciplineQuery = @"SELECT 
        dp.DisciplineId,
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
    FROM TeacherOfDiscipline dp
    JOIN Teacher tc ON tc.Id = dp.TeacherId
    INNER JOIN Passport p ON tc.PassportId = p.ID
    INNER JOIN Address a ON p.AddressId = a.ID
    INNER JOIN IdMilitary im ON tc.MilitaryId = im.ID ";
    private string _connectionString; 
    private MyLogger _logger;
    public DisciplineRepository(IGetConnectionString getConnectionString, MyLogger logger)
    {
        _connectionString = getConnectionString.ReturnConnectionString();
        _logger = logger;
    }
    public Discipline Get(long Id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            List<Teacher> teachers = db.Query<Teacher, Passport, Address, Teacher>(
                _sqlSelectTacherOfDisciplineQuery + @"WHERE dp.DisciplineId = @ID", 
                (teacher, passport, address) =>
                {
                    passport.Address = address;
                    teacher.Passport = passport;
                    return teacher;
                },
                new { Id }, splitOn: "PassportId, AddressId").ToList();
            Discipline discipline = db.Query<Discipline>(_sqlSelectDisciplineQuery + @"WHERE Id = @ID", new { Id }).First();
            discipline.Teachers = teachers;
            return discipline;
        }
    }

    public List<Discipline> ReturnList()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            List<TeacherOfDisciplineDto> teachers = db.Query<TeacherOfDisciplineDto, Teacher, Passport, Address, TeacherOfDisciplineDto>(
                _sqlSelectTacherOfDisciplineQuery, 
                (teacherOfDisciplineDto,teacher, passport, address) =>
                {
                    passport.Address = address;
                    teacher.Passport = passport;
                    teacherOfDisciplineDto.Teacher = teacher;
                    return teacherOfDisciplineDto;
                }, splitOn: "PersonId, PassportID, AddressID").ToList();
            List<Discipline> disciplines = db.Query<Discipline>(_sqlSelectDisciplineQuery).ToList();
            var dictionaryTeachers = teachers
                .GroupBy(t => t.DisciplineId)
                .ToDictionary(x => x.Key, x => x.Select(ToD => ToD.Teacher).ToList());
            foreach (var discipline in disciplines)
            {
                discipline.Teachers = dictionaryTeachers.GetValueOrDefault(discipline.DisciplineId);
            }
            return disciplines;
        }
    }
    public long Create(DisciplineDto discipline)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                string SqlQuery;
                try
                {
                    SqlQuery = @"INSERT INTO Discipline(NameDiscipline) VALUES(@NameDiscipline);
                    SELECT SCOPE_IDENTITY();";
                    discipline.DisciplineId = db.QuerySingle<int>(SqlQuery, discipline, transaction);
                    var teachers = discipline.TeacherId.Select(teacherId => new
                    {
                        DisciplineId = discipline.DisciplineId,
                        TeacherId = teacherId
                    }).ToList();
                    SqlQuery = @"INSERT INTO TeacherOfDiscipline(DisciplineId, TeacherId) VALUES(@DisciplineId, @TeacherId)";
                    db.Execute(SqlQuery,  teachers , transaction);
                    transaction.Commit();
                    return discipline.DisciplineId;
                }
                catch (Exception ex)
                {
                    _logger.Error("An error occured during transaction" + ex.Message);
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }


    public void Delete(long Id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    string SqlQuery = @"DELETE FROM Discipline WHERE ID = @ID";
                    db.Execute(SqlQuery, new { ID = Id },  transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.Error("An error occured during transaction" + ex.Message);
                    transaction.Rollback();
                }
            }   
        }
    }

    public long Update(DisciplineDto discipline)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                string SqlQuery;
                try
                {
                    SqlQuery = @"UPDATE Discipline SET NameDiscipline = @NameDiscipline WHERE ID = @DisciplineId";
                    db.Execute(SqlQuery, discipline, transaction);
                    SqlQuery = @"DELETE FROM TeacherOfDiscipline WHERE DisciplineId = @DisciplineId";
                    db.Execute(SqlQuery, discipline, transaction);
                    var teachers = discipline.TeacherId.Select(teacherId => new
                    {
                        DisciplineId = discipline.DisciplineId,
                        TeacherId = teacherId
                    }).ToList();
                    SqlQuery = @"INSERT INTO TeacherOfDiscipline(DisciplineId, TeacherId) VALUES(@DisciplineId, @TeacherId)";
                    db.Execute(SqlQuery,  teachers , transaction);
                    transaction.Commit();
                    return discipline.DisciplineId;
                }
                catch (Exception ex)
                {
                    _logger.Error("An error occured during transaction" + ex.Message);
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}