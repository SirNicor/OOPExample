namespace UCore;

public class Department
{
    public Department(string nameDepartment, Administrator headDepartment, List<Administrator> administrators, Faculty faculty)
    {
        Faculty =  faculty;
        NameDepartment = nameDepartment;
        HeadDepartment = headDepartment;
        Administrators = administrators;
    }

    public long DepartmentId { get; set; }
    public readonly Faculty Faculty;
    protected Administrator HeadDepartment;
    protected List<Administrator> Administrators;
    public readonly string NameDepartment;
}