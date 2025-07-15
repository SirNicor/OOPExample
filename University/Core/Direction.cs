namespace UCore;

public class Direction : Department
{
    static Direction()
    {
        _numberBudget = (new Random()).Next(1, 100); //замена получение данных
    }

    public Direction(string nameFaculty, Administrator dean, Administrator deputyDean, string nameDepartment,
        Administrator headDepartment, string nameDiscipline, DegreesStudy degreesStudy) : base(nameFaculty, dean, deputyDean, nameDepartment,
        headDepartment)
    {
        _nameDiscipline = nameDiscipline;
        _degreesStudy =  degreesStudy;
    }
    public DegreesStudy DegreesStudy{get{return _degreesStudy;}}
    public string ReturnGroupCipher()
    {
        return NameUniversity + " " + NameFaculty + " " + NameDepartment + " " + _nameDiscipline;
    }

    private readonly string _nameDiscipline;
    private static int _numberBudget;
    private readonly DegreesStudy _degreesStudy;
    
}