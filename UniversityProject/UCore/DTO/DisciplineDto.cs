namespace UCore;

public class DisciplineDto
{
    public long DisciplineId { get; set; }
    public string NameDiscipline { get; set; }
    public List<long> TeacherId { get; set; }
}