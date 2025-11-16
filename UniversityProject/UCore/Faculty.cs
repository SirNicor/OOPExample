namespace UCore;

public class Faculty
{
    public long FacultyId { get; set; }
    public University University { get; set; }
    public string NameFaculty { get; set; }
    public List<Administrator> AdministrationOfFaculty { get; set; }
}