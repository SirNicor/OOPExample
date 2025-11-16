namespace UCore;

public class Discipline
{
    public Discipline(string name, List<Direction> directions) 
    {
        NameDiscipline = name;
        _directions = directions;
    }
    public long DisciplineId { get; set; }
    public readonly string NameDiscipline;
    private readonly List<Direction>  _directions = new List<Direction>();
    public List<Direction> Directions{get{return _directions;}}
    //private FileInfo ProgrammDiscipline
}