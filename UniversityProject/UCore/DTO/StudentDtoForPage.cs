namespace UCore;

public class StudentDtoForPage
{
    public long? studentId { get; set; }
    public bool? criminalRecord { get; set; }
    public long? totalScore { get; set; }
    public long? skipHours { get; set; }
    public long? creditScores { get; set; }
    public long? countOfExamsPassed { get; set; }
    public long? course{get;set;}
    public long? chatId { get; set; }
    public long? addressId { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string middleName { get; set; }
    public DateTime dob { get; set; }
    public long passportId { get; set; }
    public string country { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string houseNumber { get; set; }
    public string serial { get; set; }
    public string number { get; set; }
    public string placeReceipt { get; set; }
}