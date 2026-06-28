using UCore;

namespace EFRepository;

public static class ConvertEF
{
    public static Student ConvertStudent(StudentAllDto studentRow)
    {
        Address address = new Address()
        {
            AddressId = studentRow.AddressId,
            City = studentRow.City,
            Country = studentRow.Country,
            Street = studentRow.Street,
            HouseNumber = studentRow.HouseNumber,            
        };
        Passport passport = new Passport()
        {
            PassportId = studentRow.PassportId,
            Address = address,
            BirthData = studentRow.BirthData,
            FirstName = studentRow.FirstName,
            LastName = studentRow.LastName,
            MiddleName = studentRow.MiddleName,
            Number = int.Parse(studentRow.Number),
            Serial = int.Parse(studentRow.Serial),
            PlaceReceipt = studentRow.PlaceReceipt
        };
        Student student = new Student()
        {
            ChatId = studentRow.ChatId,
            CountOfExamsPassed = (int)studentRow.CountOfExamsPassed,
            Course = (int)studentRow.CourseId,
            CreditScores = (int)studentRow.CreditScores,
            CriminalRecord = studentRow.CriminalRecord,
            MilitaryIdAvailability = Enum.Parse<IdMillitary>(studentRow.LevelId),
            Passport = passport,
            PersonId = studentRow.StudentId,
            SkipHours = (int)studentRow.SkipHours
        };
        return student;

    }
    
    public static StudentPassportAddressDto ConvertStudentToInsert(StudentDtoForPage studentRow)
    {
        EFAddress address = new EFAddress()
        {
            Id = (long)studentRow.addressId,
            AddressString = studentRow.address,
            City = studentRow.city,
            Country = studentRow.country,
            HouseNumber = studentRow.houseNumber,
            Street = studentRow.state
        };
        EFPassport passport = new EFPassport()
        {
            Id = (long)studentRow.passportId,
            Serial =  studentRow.serial,
            PlaceReceipt = studentRow.placeReceipt,
            FirstName = studentRow.firstName,
            LastName = studentRow.lastName,
            MiddleName = studentRow.middleName,
            Number = studentRow.number,
            AddressId = (long)studentRow.addressId,
            BirthData = studentRow.dob
        };
        EFStudent student = new EFStudent()
        {
            Id = (long)studentRow.studentId,
            PassportId = passport.Id,
            ChatId = Convert.ToString(studentRow.chatId),
            MilitaryId = 1,
            CriminalRecord = (bool)studentRow.criminalRecord,
            CountOfExamsPassed = studentRow.countOfExamsPassed,
            SkipHours = studentRow.skipHours,
            CreditScores = (int)studentRow.creditScores,
            CourseId = (long)studentRow.course,
        };
        return new  StudentPassportAddressDto()
        {
            Address = address, Passport = passport, Student = student
        };

    }
}