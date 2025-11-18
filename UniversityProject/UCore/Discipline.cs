namespace UCore;

public class Discipline
{
    public Discipline(string name) 
    {
        NameDiscipline = name;
    }
    public long DisciplineId { get; set; }
    public readonly string NameDiscipline;
    public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    //private FileInfo ProgrammDiscipline
}