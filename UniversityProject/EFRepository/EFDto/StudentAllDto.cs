namespace EFRepository;

public class StudentAllDto
{
    public long StudentId { get; set; }
    public long? SkipHours { get; set; }
    public long? CountOfExamsPassed { get; set; }
    public long? CreditScores { get; set; }
    public string? ChatId { get; set; }
    public bool CriminalRecord { get; set; }
    public long CourseId { get; set; }
    public string LevelDegrees { get; set; }
    
    public long PassportId { get; set; }
    public string Serial { get; set; }
    public string Number { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateOnly BirthData { get; set; }
    public string PlaceReceipt { get; set; }
    
    public long AddressId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string Address { get; set; }
    
    public long MilitaryId { get; set; }
    public string LevelId { get; set; }
}

