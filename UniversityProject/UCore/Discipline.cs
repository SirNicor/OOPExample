namespace UCore;

public class Discipline
{
    public Discipline(string name) 
    {
        NameDiscipline = name;
    }

    public Discipline()
    {
        
    }
    public long DisciplineId { get; set; }
    public string NameDiscipline{ get; set; }
    public List<Teacher> Teachers { get; set; }
    //private FileInfo ProgrammDiscipline
}