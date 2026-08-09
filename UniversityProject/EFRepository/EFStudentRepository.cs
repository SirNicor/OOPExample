using IRepositoryAll;
using Logger;
using Microsoft.EntityFrameworkCore;
using UCore;
using static Microsoft.EntityFrameworkCore.EF;
using System.Linq.Dynamic.Core;
using Ucore;

namespace EFRepository;

public class EFStudentRepository : IStudentRepository
{
    private readonly MyLogger _logger;
    private readonly UniversityDbContext _db;

    public EFStudentRepository(MyLogger logger, UniversityDbContext db)
    {
        _logger = logger;
        _db = db;
    }
    
    public async Task PrintAllAsync()
    {
        var students = await(_db.Students
            .Select(s => new Student()
            {
                Passport = new Passport()
                {
                    Address = new Address()
                    {
                        AddressId = s.Passport.Address.AddressId,
                        Country = s.Passport.Address.Country,
                        City = s.Passport.Address.City,
                        Street = s.Passport.Address.Street,
                        HouseNumber = s.Passport.Address.HouseNumber,
                        AddressString = s.Passport.Address.AddressString
                    },
                    Serial = s.Passport.Serial,
                    Number = s.Passport.Number,
                    FirstName =  s.Passport.FirstName,
                    LastName =  s.Passport.LastName,
                    MiddleName =  s.Passport.MiddleName,
                    BirthData = s.Passport.BirthData,
                    AddressId = s.Passport.AddressId,
                    PlaceReceipt = s.Passport.PlaceReceipt,
                    PassportId = s.Passport.PassportId,
                },
                StudentId = s.Id,
                Id = s.Id,
                ChatId =  s.ChatId,
                CountOfExamsPassed = s.CountOfExamsPassed,
                Course = s.Course,
                CreditScores = s.CreditScores,
                CriminalRecord = s.CriminalRecord,
                MillitaryId = s.MillitaryId,
                Millitary = s.Millitary,
                PassportId = s.Passport.PassportId,
                SkipHours = s.SkipHours,
            }).ToListAsync());
        foreach (var st in students)
        {
            st.PrintInfo(_logger);
        }
    }

    public async Task<List<Student>> ReturnListAsync()
    {
        var students = await(_db.Students
            .Select(s => new Student()
            {
                Passport = new Passport()
                {
                    Address = new Address()
                    {
                        AddressId = s.Passport.Address.AddressId,
                        Country = s.Passport.Address.Country,
                        City = s.Passport.Address.City,
                        Street = s.Passport.Address.Street,
                        HouseNumber = s.Passport.Address.HouseNumber,
                        AddressString = s.Passport.Address.AddressString
                    },
                    Serial = s.Passport.Serial,
                    Number = s.Passport.Number,
                    FirstName =  s.Passport.FirstName,
                    LastName =  s.Passport.LastName,
                    MiddleName =  s.Passport.MiddleName,
                    BirthData = s.Passport.BirthData,
                    AddressId = s.Passport.AddressId,
                    PlaceReceipt = s.Passport.PlaceReceipt,
                    PassportId = s.Passport.PassportId,
                },
                StudentId = s.Id,
                Id = s.Id,
                ChatId =  s.ChatId,
                CountOfExamsPassed = s.CountOfExamsPassed,
                Course = s.Course,
                CreditScores = s.CreditScores,
                CriminalRecord = s.CriminalRecord,
                MillitaryId = s.MillitaryId,
                Millitary = s.Millitary,
                PassportId = s.Passport.PassportId,
                SkipHours = s.SkipHours,
            }).ToListAsync());
        return students;
    }

    public async Task<long> CreateAsync(StudentDtoForPage student)
    {
        await using var transaction = _db.Database.BeginTransaction();
        var insertDate = ConvertEF.ConvertStudentToInsert(student);
        var studentRow = insertDate.Student;
        studentRow.Passport = insertDate.Passport;
        studentRow.Passport.Address = insertDate.Address;
        _db.Students.Add(studentRow);
        await _db.SaveChangesAsync();
        await transaction.CommitAsync();
        return (long)studentRow.StudentId;
    }

    public async Task<long?> UpdateAsync(StudentDtoForPage student)
    {
        await using var transaction = _db.Database.BeginTransaction();
        var insertDate = ConvertEF.ConvertStudentToInsert(student);
        var studentRow = insertDate.Student;
        studentRow.Passport = insertDate.Passport;
        studentRow.Passport.Address = insertDate.Address;
        _db.Students.Update(studentRow);
        await _db.SaveChangesAsync();
        await transaction.CommitAsync();
        return studentRow.StudentId;
    }

    public async Task DeleteAsync(long ID)
    {
        _db.Students.Remove(await _db.Students.FindAsync(ID));
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAddressAsync(long ID)
    {
        _db.Addresses.Remove(await _db.Addresses.FindAsync(ID));
        await _db.SaveChangesAsync();
    }

    public async Task DeletePassportAsync(long ID)
    {
        _db.Passports.Remove(await _db.Passports.FindAsync(ID));
        await _db.SaveChangesAsync();
    }

    public async Task<Student> GetAsync(long ID)
    {
        var student = await(_db.Students
            .Where(s => s.StudentId == ID)
            .Select(s => new Student()
            {
                Passport = new Passport()
                {
                    Address = new Address()
                    {
                        AddressId = s.Passport.Address.AddressId,
                        Country = s.Passport.Address.Country,
                        City = s.Passport.Address.City,
                        Street = s.Passport.Address.Street,
                        HouseNumber = s.Passport.Address.HouseNumber,
                        AddressString = s.Passport.Address.AddressString
                    },
                    Serial = s.Passport.Serial,
                    Number = s.Passport.Number,
                    FirstName =  s.Passport.FirstName,
                    LastName =  s.Passport.LastName,
                    MiddleName =  s.Passport.MiddleName,
                    BirthData = s.Passport.BirthData,
                    AddressId = s.Passport.AddressId,
                    PlaceReceipt = s.Passport.PlaceReceipt,
                    PassportId = s.Passport.PassportId,
                },
                StudentId = s.Id,
                Id = s.Id,
                ChatId =  s.ChatId,
                CountOfExamsPassed = s.CountOfExamsPassed,
                Course = s.Course,
                CreditScores = s.CreditScores,
                CriminalRecord = s.CriminalRecord,
                MillitaryId = s.MillitaryId,
                Millitary = s.Millitary,
                PassportId = s.Passport.PassportId,
                SkipHours = s.SkipHours,
            }).FirstOrDefaultAsync());
        return student;
    }

    public async Task<StudentDtoForPage> GetStudentPageAsync(long studentId)
    {
        var studentPage = await _db.Students
            .Select(s => new StudentDtoForPage()
            {
                studentId = s.Id,
                criminalRecord = s.CriminalRecord,
                skipHours = s.SkipHours,
                creditScores = s.CreditScores,
                countOfExamsPassed = s.CountOfExamsPassed,
                course =  s.Course,
                chatId = Convert.ToInt32(s.ChatId),    
                address = s.Passport.Address.AddressString,
                addressId = s.Passport.Address.AddressId,
                firstName = s.Passport.FirstName,
                lastName = s.Passport.LastName,
                middleName = s.Passport.MiddleName,
                dob = s.Passport.BirthData,
                passportId =  s.Passport.PassportId,
                country = s.Passport.Address.Country,
                city = s.Passport.Address.City,
                state = s.Passport.Address.Street,
                houseNumber = s.Passport.Address.HouseNumber,
                serial = Convert.ToString(s.Passport.Serial),
                number = Convert.ToString(s.Passport.Number),
                placeReceipt = s.Passport.PlaceReceipt
            }).Where(s => s.studentId == studentId).FirstOrDefaultAsync();
        return studentPage;
    }

    public async Task<(List<StudentTableDTO>, long)> GetStudentTableDTO(long FirstId, long count, string? SortColumn, string? SortOrder, FilterDto? filter, CancellationToken token)
    {
        SortOrder = SortOrder == "null"? "ASC" : SortOrder;
        SortColumn = SortColumn == "null" ? "Id" : SortColumn;
        IQueryable<Student> queryable = _db.Students.AsNoTracking();
        if (filter.FilterCourse is not null)
        {
            long numberOfCourse = (long)filter.FilterCourse;
            queryable = queryable.Where(student => student.Course == filter.FilterCourse);
        }

        if (filter.FilterDate[0] != "")
        {
            var filterDateStart = DateOnly.FromDateTime(Convert.ToDateTime(filter.FilterDate[0]));
            var filterDateEnd = DateOnly.FromDateTime(Convert.ToDateTime(filter.FilterDate[1]));
            queryable = queryable.Where(student => student.Passport.BirthData >= filterDateStart & student.Passport.BirthData <= filterDateEnd);    
        }

        if (filter.FilterSkipHoursEnd is not null && filter.FilterSkipHoursStart is not null)
        {
            queryable = queryable.Where(student => student.SkipHours >= filter.FilterSkipHoursStart & student.SkipHours <= filter.FilterSkipHoursEnd);
        }
        if (filter.FilterTotalScore is not null)
        {
            
        }
        var countAsync = await queryable.CountAsync(token);
        queryable = queryable.OrderBy($"{SortColumn} {SortOrder}");
        queryable = queryable.Skip((int)(FirstId)).Take((int)count);  
        var st = await (queryable.Select(s => new StudentTableDTO()
        {
            studentId = s.Id,
            Fio = s.Passport.FirstName + " " + s.Passport.LastName + " " + s.Passport.MiddleName,
            Dob = s.Passport.BirthData,
            Address = s.Passport.Address.AddressString,
            Serial = s.Passport.Serial,
            Number = s.Passport.Number,
            TotalScore = s.CountOfExamsPassed > 0 ? (double?)s.CreditScores / s.CountOfExamsPassed : 0,
            SkipHours =  s.SkipHours??0,
            CreditScore = s.CreditScores??0,
            Course =  (int)s.Course,
            CountOfExamsPassed =  s.CountOfExamsPassed??0
        }).ToListAsync(token));
        return (st, countAsync);
    }

    public async Task<long> GetCountAsync()
    {
        return await _db.Students.CountAsync();
    }

    public async Task<Student?> GetStudentForChatIdAsync(string chatId)
    {
        throw new NotImplementedException();
    }

    public async Task<long?> CheckNameAsync(string firstName, string LastName)
    {
        var id = await(_db.Students.Where(s => s.Passport.FirstName == firstName && s.Passport.LastName == LastName).Select(s => s.StudentId).FirstOrDefaultAsync());
        return id;
    }
}