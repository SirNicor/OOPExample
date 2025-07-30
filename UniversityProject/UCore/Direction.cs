namespace University.UCore;

public class Direction 
{
    static Direction()
    {
        
    }

    public Direction(Department department, string nameDirection, DegreesStudy degreesStudy, List<Discipline> disciplines, List<Student> students)
    {
        _department = department;
        NameDirection = nameDirection;
        DegreesStudy =  degreesStudy;
        _disciplines = disciplines;
        _students = students;
    }

    public string ReturnGroupCipher()
    {
        return $"{_department.Faculty.University.NameUniversity}.{_department.Faculty.NameFaculty}.{_department.NameDepartment}.{NameDirection}.courses:{_numberOfCourse}";
    }
    
    public DegreesStudy DegreesStudy { get; }
    public readonly string NameDirection;
    private readonly Department _department;
    private int _numberOfCourse;
    private List<Discipline> _disciplines;
    private List<Student> _students;
}