namespace UCore;

public class DirectionDto
{
    public DegreesStudy DegreesStudy { get; set; }

    public string NameDirection { get; set; }
    public long DepartmentId { get; set; }
    public long DirectionId { get; set; }
    public string ChatId { get; set; }
    public int NumberOfCourse { get; set; }
    public List<int> StudentsId { get; set; }
    public List<int> DisciplineId { get; set; }
}