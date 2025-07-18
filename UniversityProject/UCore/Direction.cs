namespace University.UCore;

public class Direction 
{
    static Direction()
    {
        _numberBudget = (new Random()).Next(1, 100); //замена получение данных
    }

    public Direction(Department department, string nameDirection, DegreesStudy degreesStudy)
    {
        Department = department;
        _nameDirection = nameDirection;
        _degreesStudy =  degreesStudy;
    }
    public DegreesStudy DegreesStudy{get{return _degreesStudy;}}
    public string ReturnGroupCipher()
    {
        return $"{Department.Faculty.University.NameUniversity}.{Department.Faculty.NameFaculty}.{Department.NameDepartment}.{_nameDirection}.courses:{NumberOfCourse}";
    }

    public readonly Department Department;
    protected readonly string _nameDirection;
    protected static int _numberBudget;
    protected int NumberOfCourse;
    protected readonly DegreesStudy _degreesStudy;
    
}