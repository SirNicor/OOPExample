namespace University.UCore;

public class Faculty
{
    public Faculty(string nameFaculty, Administrator dean, Administrator deputyDean, List<Administrator> administrationOfDeanOffice,ClassUniversity university) 
    {
        University = university;
        NameFaculty = nameFaculty;
        Dean = dean;
        AdministrationOfDeanOffice = administrationOfDeanOffice;
        DeputyDean = deputyDean;
    }
    public string ReturnNameFaculty { get; }
    
    public readonly ClassUniversity University;
    public readonly string NameFaculty;  
    protected Administrator Dean;
    protected Administrator DeputyDean;
    protected List<Administrator> AdministrationOfDeanOffice;
}