namespace UCore;

public class Faculty
{
    public Faculty(string nameFaculty, List<Administrator> administrationOfFaculty,University university) 
    {
        University = university;
        NameFaculty = nameFaculty;
        AdministrationOfFaculty = administrationOfFaculty;
    }

    public Faculty()
    {
        
    }
    public long FacultyId { get; set; }
    public University University { get; set; }
    public string NameFaculty { get; set; }
    public List<Administrator> AdministrationOfFaculty { get; set; }
}