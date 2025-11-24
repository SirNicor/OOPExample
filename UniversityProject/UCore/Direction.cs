namespace UCore;

public class Direction 
{
    public Direction()
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
    
    public DegreesStudy DegreesStudy { get; set; }

    public string NameDirection { get; set; }
    public Department Department { get; set; }
    public long DirectionId { get; set; }
    private string _chatId;
    public string ChatId
    {
        get { return _chatId;}
        set
        {
            if (value != null)
            {
                _chatId = value;
            }
            else
            {
                _chatId = "";
            }
        }
        
    }
    public int NumberOfCourse { get; set; }
    public List<Student> Students { get; set; }
    public List<Discipline> Disciplines { get; set; }
}