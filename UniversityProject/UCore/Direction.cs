namespace UCore;

public class Direction 
{
    static Direction()
    {
        
    }

    public Direction(Department department, string nameDirection, DegreesStudy degreesStudy, List<Student> students)
    {
        Department = department;
        NameDirection = nameDirection;
        DegreesStudy =  degreesStudy;    
        Students = students;
    }

    public string ReturnGroupCipher()
    {
        return $"{Department.Faculty.University.NameUniversity}.{Department.Faculty.NameFaculty}.{Department.NameDepartment}.{NameDirection}.courses:{NumberOfCourse}";
    }
    
    public DegreesStudy DegreesStudy { get; }

    public string NameDirection { get; set; }
    public Department Department { get; set; }
    public long DirectionId { get; set; }
    public string ChatId { get; set; }
    public int NumberOfCourse { get; set; }
    public List<Student> Students { get; set; }
    public List<Discipline> Disciplines { get; set; }
}