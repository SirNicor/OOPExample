namespace University;

public class Faculty:ClassUniversity
{
    public Faculty(string nameFaculty, Administrator dean, Administrator deputyDean)
    {
        NameFaculty = nameFaculty;
        Dean = dean;
        DeputyDean = deputyDean;
    }
    public string ReturnNameFaculty { get; }
    
    protected readonly string NameFaculty;
    protected Administrator Dean;
    protected Administrator DeputyDean;
}