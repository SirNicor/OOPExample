namespace UCore;

public class DegreesStudyClass
{
    public int Id { get; set; }
    public string? LevelDegrees { get; set; } = null!;
    public ICollection<Person> Persons { get; set; } = new List<Person>();
}