namespace UCore;

public class UniversityDto
{
    public int IdUniversity { get; set; }
    public string NameUniversity { get; set; }
    public int IdRector { get; set; }
    public List<int> IdAdministrators { get; set; }
    public int BudgetSize { get; set; }
}