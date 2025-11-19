namespace UCore;

public class Discipline
{
    public Discipline()
    {
        
    }
    public long DisciplineId { get; set; }
    public string NameDiscipline{ get; set; }
    public List<Teacher> Teachers { get; set; }
    //private FileInfo ProgrammDiscipline
}