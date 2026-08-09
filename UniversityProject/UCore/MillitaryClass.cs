namespace UCore;

public class MillitaryClass
{
    public int MillitaryId { get; set; }
    public string LevelId { get; set; }
    public ICollection<Person> Person { get; set; } = new List<Person>();
}