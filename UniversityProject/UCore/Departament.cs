namespace University.UCore;

public class Department:Faculty
{
    public Department(string nameFaculty, Administrator dean, Administrator deputyDean, string nameDepartment, Administrator headDepartment):base(nameFaculty, dean, deputyDean)
    {
        NameDepartment = nameDepartment;
        HeadDepartment = headDepartment;
    }
    
    protected readonly string NameDepartment;
    protected Administrator HeadDepartment;
}