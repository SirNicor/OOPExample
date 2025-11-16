namespace UCore;

public class Department
{
    public Department(string nameDepartment, List<Administrator> administrators, Faculty faculty)
    {
        Faculty =  faculty;
        NameDepartment = nameDepartment;
        Administrators = administrators;
    }

    public long DepartmentId { get; set; }
    public readonly Faculty Faculty;
    protected List<Administrator> Administrators;
    public readonly string NameDepartment;
}