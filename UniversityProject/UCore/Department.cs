namespace UCore;

public class Department
{
    public long DepartmentId { get; set; }
    public Faculty Faculty { get; set; }
    public List<Administrator> Administrators { get; set; }
    public string NameDepartment { get; set; }
}