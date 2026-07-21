namespace UCore;

public class IdMillitaryClass
{
    public int Id { get; set; }

    public IdMillitary LevelId { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}