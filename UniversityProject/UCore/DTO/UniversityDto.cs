namespace UCore;

public class UniversityDto
{
    public long IdUniversity { get; set; }
    public string NameUniversity { get; set; }
    public long IdRector { get; set; }
    public List<long> IdAdministrators { get; set; }
    public long BudgetSize { get; set; }
}