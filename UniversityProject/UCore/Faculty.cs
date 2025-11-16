namespace UCore;

public class Faculty
{
    public Faculty(string nameFaculty, Administrator dean, Administrator deputyDean, List<Administrator> administrationOfDeanOffice,University university) 
    {
        University = university;
        NameFaculty = nameFaculty;
        Dean = dean;
        AdministrationOfDeanOffice = administrationOfDeanOffice;
        DeputyDean = deputyDean;
    }
    public string ReturnNameFaculty { get; }
    public long FacultyId { get; set; }
    public readonly University University;
    public readonly string NameFaculty;  
    protected Administrator Dean;
    protected Administrator DeputyDean;
    protected List<Administrator> AdministrationOfDeanOffice;
}