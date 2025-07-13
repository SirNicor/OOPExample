namespace UCore;

public class Discipline:Department
{
    static Discipline()
    {
        NumberBudget = (new Random()).Next(1, 100); //замена получение данных
    }
    public Discipline(string nameFaculty, Administrator dean, Administrator deputyDean, string nameDepartment, Administrator headDepartment, string nameDiscipline):base(nameFaculty, dean, deputyDean,nameDepartment, headDepartment)
    {
        _nameDiscipline = nameDiscipline;
    }
    public string ReturnGroupCipher()
    {
        return NameUniversity + " " + NameFaculty + " " + NameDepartment + " " + _nameDiscipline;
    }
    
    private readonly string _nameDiscipline;
    private static int NumberBudget;
}