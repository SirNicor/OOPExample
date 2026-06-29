using IRepositoryAll;
using Logger;
using Microsoft.EntityFrameworkCore;
using UCore;
using static Microsoft.EntityFrameworkCore.EF;
using System.Linq.Dynamic.Core;

namespace EFRepository;

public class EFStudentRepository : IStudentRepository
{
    private readonly MyLogger _logger;

    public EFStudentRepository(MyLogger logger)
    {
        _logger = logger;
    }
    
    public async Task PrintAllAsync()
    {
        await using UniversityDbContext db = new UniversityDbContext();
        var students = await (
            from student in db.Students
            join passport in db.Passports on student.PassportId equals passport.Id
            join address in db.Addresses on passport.AddressId equals address.Id
            join degrees in db.DegreesStudies on student.CourseId equals degrees.Id
            join military in db.IdMilitaries on student.MilitaryId equals military.Id
            select new StudentAllDto
            {
                StudentId = student.Id,
                SkipHours = student.SkipHours,
                CountOfExamsPassed = student.CountOfExamsPassed,
                CreditScores = student.CreditScores,
                ChatId = student.ChatId,
                CriminalRecord = student.CriminalRecord,
                CourseId = student.CourseId,
                LevelDegrees = degrees.LevelDegrees,
                PassportId = passport.Id,
                Serial = passport.Serial,
                Number = passport.Number,
                FirstName = passport.FirstName,
                LastName = passport.LastName,
                MiddleName = passport.MiddleName,
                BirthData = passport.BirthData,
                AddressId = address.Id,
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
                MilitaryId = military.Id,
                LevelId = military.LevelId
            }).ToListAsync();
        foreach (var st in students)
        {
            ConvertEF.ConvertStudent(st).PrintInfo(_logger);
        }
    }

    public async Task<List<Student>> ReturnListAsync()
    {
        await using UniversityDbContext db = new UniversityDbContext();
        var students = await (
            from student in db.Students
            join passport in db.Passports on student.PassportId equals passport.Id
            join address in db.Addresses on passport.AddressId equals address.Id
            join degrees in db.DegreesStudies on student.CourseId equals degrees.Id
            join military in db.IdMilitaries on student.MilitaryId equals military.Id
            select new StudentAllDto
            {
                StudentId = student.Id,
                SkipHours = student.SkipHours,
                CountOfExamsPassed = student.CountOfExamsPassed,
                CreditScores = student.CreditScores,
                ChatId = student.ChatId,
                CriminalRecord = student.CriminalRecord,
                CourseId = student.CourseId,
                LevelDegrees = degrees.LevelDegrees,
                PassportId = passport.Id,
                Serial = passport.Serial,
                Number = passport.Number,
                FirstName = passport.FirstName,
                LastName = passport.LastName,
                MiddleName = passport.MiddleName,
                BirthData = passport.BirthData,
                AddressId = address.Id,
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
                MilitaryId = military.Id,
                LevelId = military.LevelId
            }).ToListAsync();
        List<Student> allStudent = new List<Student>();
        foreach (var st in students)
        {
            allStudent.Add(ConvertEF.ConvertStudent(st));
        }
        return allStudent;
    }

    public async Task<long> CreateAsync(StudentDtoForPage student)
    {
        await using var db =  new UniversityDbContext();
        var insertDate = ConvertEF.ConvertStudentToInsert(student);
        var studentRow = insertDate.Student;
        studentRow.EfPassport = insertDate.Passport;
        studentRow.EfPassport.EfAddress = insertDate.Address;
        db.Students.Add(studentRow);
        await db.SaveChangesAsync();
        return studentRow.Id;
    }

    public async Task<long?> UpdateAsync(StudentDtoForPage student)
    {
        await using var db =  new UniversityDbContext();
        var insertDate = ConvertEF.ConvertStudentToInsert(student);
        var studentRow = insertDate.Student;
        studentRow.EfPassport = insertDate.Passport;
        studentRow.EfPassport.EfAddress = insertDate.Address;
        db.Students.Update(studentRow);
        await db.SaveChangesAsync();
        return studentRow.Id;
    }

    public async Task DeleteAsync(long ID)
    {
        await using var db =  new UniversityDbContext();
        db.Students.Remove(await db.Students.FindAsync(ID));
        await db.SaveChangesAsync();
    }

    public async Task DeleteAddressAsync(long ID)
    {
        await using var db =  new UniversityDbContext();
        db.Addresses.Remove(await db.Addresses.FindAsync(ID));
        await db.SaveChangesAsync();
    }

    public async Task DeletePassportAsync(long ID)
    {
        await using var db =  new UniversityDbContext();
        db.Passports.Remove(await db.Passports.FindAsync(ID));
        await db.SaveChangesAsync();
    }

    public async Task<Student> GetAsync(long ID)
    {
        await using UniversityDbContext db = new UniversityDbContext();
        var students = await (
            from student in db.Students
            join passport in db.Passports on student.PassportId equals passport.Id
            join address in db.Addresses on passport.AddressId equals address.Id
            join degrees in db.DegreesStudies on student.CourseId equals degrees.Id
            join military in db.IdMilitaries on student.MilitaryId equals military.Id
            where student.Id == ID
            select new StudentAllDto
            {
                StudentId = student.Id,
                SkipHours = student.SkipHours,
                CountOfExamsPassed = student.CountOfExamsPassed,
                CreditScores = student.CreditScores,
                ChatId = student.ChatId,
                CriminalRecord = student.CriminalRecord,
                CourseId = student.CourseId,
                LevelDegrees = degrees.LevelDegrees,
                PassportId = passport.Id,
                Serial = passport.Serial,
                Number = passport.Number,
                FirstName = passport.FirstName,
                LastName = passport.LastName,
                MiddleName = passport.MiddleName,
                BirthData = passport.BirthData,
                AddressId = address.Id,
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
                MilitaryId = military.Id,
                LevelId = military.LevelId
            }).FirstOrDefaultAsync();
        return ConvertEF.ConvertStudent(students);
    }

    public async Task<StudentDtoForPage> GetStudentPageAsync(long studentId)
    {
        await using UniversityDbContext db = new UniversityDbContext();
        var students = await (
            from student in db.Students
            join passport in db.Passports on student.PassportId equals passport.Id
            join address in db.Addresses on passport.AddressId equals address.Id
            join degrees in db.DegreesStudies on student.CourseId equals degrees.Id
            join military in db.IdMilitaries on student.MilitaryId equals military.Id
            where student.Id == studentId
            select new StudentDtoForPage
            {
                studentId = student.Id,
                criminalRecord = student.CriminalRecord,
                skipHours = student.SkipHours,
                creditScores = student.CreditScores,
                countOfExamsPassed = student.CountOfExamsPassed,
                course =  student.CourseId,
                chatId = Convert.ToInt32(student.ChatId),    
                address = address.AddressString,
                addressId = address.Id,
                firstName = passport.FirstName,
                lastName = passport.LastName,
                middleName = passport.MiddleName,
                dob = passport.BirthData,
                passportId =  passport.Id,
                country = address.Country,
                city = address.City,
                state = address.Street,
                houseNumber = address.HouseNumber,
                serial = passport.Serial,
                number = passport.Number,
                placeReceipt = passport.PlaceReceipt
            }).FirstOrDefaultAsync();
        return students;
    }

    public async Task<(List<StudentTableDTO>, long)> GetStudentTableDTO(long FirstId, long count, string? SortColumn, string? SortOrder, FilterDto? filter)
    {
        await using UniversityDbContext db = new UniversityDbContext();
        SortOrder = SortOrder == "null"? "ASC" : SortOrder;
        SortColumn = SortColumn == "null" ? "studentId" : SortColumn;
        var students = (
            from student in db.Students
            join passport in db.Passports on student.PassportId equals passport.Id
            join address in db.Addresses on passport.AddressId equals address.Id
            join degrees in db.DegreesStudies on student.CourseId equals degrees.Id
            join military in db.IdMilitaries on student.MilitaryId equals military.Id
            select new StudentTableDTO
            {
                studentId = student.Id,
                Fio = passport.FirstName + " " + passport.LastName + " " + passport.MiddleName,
                Dob = passport.BirthData,
                Address = address.AddressString,
                Serial = Convert.ToInt32(passport.Serial),
                Number = Convert.ToInt32(passport.Number),
                TotalScore = student.CountOfExamsPassed > 0 ? student.CreditScores as double? / student.CountOfExamsPassed : 0,
                SkipHours =  Convert.ToInt32(student.SkipHours??0),
                CreditScore = Convert.ToInt32(student.CreditScores??0),
                Course =  Convert.ToInt32(student.CourseId),
                CountOfExamsPassed =  Convert.ToInt32(student.CountOfExamsPassed??0),
            });
        if (filter.FilterCourse is not null)
        {
            long numberOfCourse = (long)filter.FilterCourse;
            students = students.Where(student => student.Course == filter.FilterCourse);
        }

        if (filter.FilterDate[0] != "")
        {
            var filterDateStart = DateOnly.FromDateTime(Convert.ToDateTime(filter.FilterDate[0]));
            var filterDateEnd = DateOnly.FromDateTime(Convert.ToDateTime(filter.FilterDate[1]));
            students = students.Where(student => student.Dob >= filterDateStart & student.Dob <= filterDateEnd);    
        }

        if (filter.FilterSkipHoursEnd is not null && filter.FilterSkipHoursStart is not null)
        {
            students = students.Where(student => student.SkipHours >= filter.FilterSkipHoursStart & student.SkipHours <= filter.FilterSkipHoursEnd);
        }
        if (filter.FilterTotalScore is not null)
        {
            
        }
        var countAsync = await students.CountAsync();

        students = students.OrderBy($"{SortColumn} {SortOrder}");
        students = students.Skip((int)(FirstId)).Take((int)count);  
        var st = await students.ToListAsync();
        return (st, countAsync);
    }

    public async Task<long> GetCountAsync()
    {
        await using UniversityDbContext db = new UniversityDbContext();
        return await db.Students.CountAsync();
    }

    public async Task<Student?> GetStudentForChatIdAsync(string chatId)
    {
        await using UniversityDbContext db = new UniversityDbContext();
        var students = await (
            from student in db.Students
            join passport in db.Passports on student.PassportId equals passport.Id
            join address in db.Addresses on passport.AddressId equals address.Id
            join degrees in db.DegreesStudies on student.CourseId equals degrees.Id
            join military in db.IdMilitaries on student.MilitaryId equals military.Id
            where student.ChatId == chatId
            select new StudentAllDto
            {
                StudentId = student.Id,
                SkipHours = student.SkipHours,
                CountOfExamsPassed = student.CountOfExamsPassed,
                CreditScores = student.CreditScores,
                ChatId = student.ChatId,
                CriminalRecord = student.CriminalRecord,
                CourseId = student.CourseId,
                LevelDegrees = degrees.LevelDegrees,
                PassportId = passport.Id,
                Serial = passport.Serial,
                Number = passport.Number,
                FirstName = passport.FirstName,
                LastName = passport.LastName,
                MiddleName = passport.MiddleName,
                BirthData = passport.BirthData,
                AddressId = address.Id,
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
                MilitaryId = military.Id,
                LevelId = military.LevelId
            }).FirstOrDefaultAsync();
        return ConvertEF.ConvertStudent(students);
    }

    public async Task<long?> CheckNameAsync(string firstName, string LastName)
    {
        await using var db = new UniversityDbContext();
        long id = await (
            from student in db.Students
            join passport in db.Passports on student.PassportId equals passport.Id
            where passport.FirstName == firstName && passport.LastName == LastName
            select student.Id).FirstOrDefaultAsync();
        return id;
    }
}