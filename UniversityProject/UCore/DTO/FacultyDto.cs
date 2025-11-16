namespace UCore;

public class FacultyDto
{
    public long IdFaculty { get; set; }
    public long IdUniversity { get; set; }
    public string NameFaculty { get; set; }
    public List<long> IdAdministrators { get; set; }
}