namespace University.UCore;

public class Department
{
    public Department(string nameDepartment, Administrator headDepartment, Faculty faculty)
    {
        Faculty =  faculty;
        NameDepartment = nameDepartment;
        HeadDepartment = headDepartment;
    }
    
    public readonly Faculty Faculty;
    protected Administrator HeadDepartment;
    protected Administrator[] Administrators;
    public readonly string NameDepartment;
}