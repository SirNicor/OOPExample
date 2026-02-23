using System.Security.AccessControl;

namespace UCore;

public class StudentTableDTO
{
    public long studentId { get; set; }
    public string Fio { get; set; }
    public DateTime Dob { get; set; }
    public string Address { get; set; }
    public int Serial { get; set; }
    public int Number { get; set; }
    public double? TotalScore { get; set; }
    public int SkipHours { get; set; }
    public int CreditScore { get; set; }
    public int Course { get; set; }
    public int CountOfExamsPassed;
}