namespace University;

public class ClassUniversity
{
     public const string NameUniversity = "NameOfUniversity";
     
}
public class Faculty:ClassUniversity
{
    public Faculty(string nameFaculty, Administrator Dean, Administrator deputyDean)
    {
        NameFaculty = nameFaculty;
        this.Dean = Dean;
        Deputy_Dean = deputyDean;
    }
     protected string NameFaculty;
     protected Administrator Dean;
     protected Administrator Deputy_Dean;
     public string ReturnNameFaculty { get; }
}
public class Department:Faculty
{
    public Department(string NameFaculty, Administrator Dean, Administrator deputyDean, string nameDepartment, Administrator HeadDepartment):base(NameFaculty, Dean, deputyDean)
    {
        NameDepartment = nameDepartment;
        this.HeadDepartment = HeadDepartment;
    }
    protected string NameDepartment;
    protected Administrator HeadDepartment;
    public string ReturnNameDepartment { get; }
}
public class Discipline:Department
{
    static Discipline()
    {
        NumberBudget = (new Random()).Next(1, 100); //замена получение данных
    }
    public Discipline(string NameFaculty, Administrator Dean, Administrator deputyDean, string nameDepartment, Administrator HeadDepartment, string nameDiscipline):base(NameFaculty, Dean, deputyDean,nameDepartment, HeadDepartment)
    {
        NameDiscipline = nameDiscipline;
    }
    private string NameDiscipline;
    private static int NumberBudget;
    public string ReturnNameDiscipline { get; }
    public string ReturnGroupCipher()
    {
        return ClassUniversity.NameUniversity + " " + NameFaculty + " " + NameDepartment + " " + NameDiscipline;
    }
}
public class Dormitories:ClassUniversity
{
    
}