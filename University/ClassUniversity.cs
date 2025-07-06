namespace University;

public class ClassUniversity
{
     public const string NameUniversity = "University";
}
public class Faculty:ClassUniversity
{
     protected string NameFaculty;
     protected Administrator Dean;
     protected Administrator Deputy_Dean;
     public string ReturnNameFaculty { get; }
}
public class Department:Faculty
{
    protected string NameDepartment;
    protected Administrator HeadDepartment;
    public string ReturnNameDepatment { get; }
}
public class Discipline:Department
{
    protected string NameDiscipline;
    protected int NumberBudget;
    public string ReturnNameDiscipline { get; }
}
public class Dormitories:ClassUniversity
{
    
}