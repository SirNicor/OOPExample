using System.Security.AccessControl;

namespace UCore;

public class StudentTableDTO
{
    public long studentId { get; set; }
    public string Fio { get; set; }
    public DateOnly Dob { get; set; }
    public string Address { get; set; }
    public string Serial { get; set; }
    public string Number { get; set; }
    public double? TotalScore { get; set; }
    public long SkipHours { get; set; }
    public long CreditScore { get; set; }
    public long Course { get; set; }
    public long CountOfExamsPassed;
}