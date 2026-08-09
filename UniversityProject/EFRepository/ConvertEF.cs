using Ucore;
using UCore;

namespace EFRepository;

public static class ConvertEF
{
    public static StudentPassportAddressDto ConvertStudentToInsert(StudentDtoForPage studentRow)
    {
        Address address = new Address
        {
            AddressId = (studentRow.addressId == null ? 0 : (long)studentRow.addressId),
            AddressString = studentRow.address,
            City = studentRow.city,
            Country = studentRow.country,
            HouseNumber = studentRow.houseNumber,
            Street = studentRow.state
        };
        Passport passport = new Passport
        {
            PassportId = (studentRow.passportId == null ? 0 : (long)studentRow.passportId),
            Serial = studentRow.serial,
            PlaceReceipt = studentRow.placeReceipt,
            FirstName = studentRow.firstName,
            LastName = studentRow.lastName,
            MiddleName = studentRow.middleName,
            Number = studentRow.number,
            BirthData = studentRow.dob
        };
        Student student = new Student
        {
            StudentId = (studentRow.studentId == null ? 0 : (long)studentRow.studentId),
            ChatId = Convert.ToString(studentRow.chatId),
            Millitary = new MillitaryClass(){MillitaryId = 1, LevelId = "DidNotServe"},
            CriminalRecord = (bool)studentRow.criminalRecord,
            CountOfExamsPassed = (int)studentRow.countOfExamsPassed,
            SkipHours = (int)studentRow.skipHours,
            CreditScores = (studentRow.creditScores == null ? 0 : (int)studentRow.creditScores),
            Course = (int)studentRow.course,
        };
        return new StudentPassportAddressDto()
        {
            Address = address, Passport = passport, Student = student
        };
    }
}