namespace UCore;

public class DepartmentDto
{
    public long DepartmentId { get; set; }
    public long FacultyId { get; set; }
    public string NameDepartment { get; set; }
    public List<long> IdAdministrators { get; set; }
}
