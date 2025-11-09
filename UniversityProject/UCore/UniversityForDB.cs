namespace UCore;

public class UniversityForDB
{
    public string NameUniversity { get; set; }
    public int IdRector { get; set; }
    public List<int> IdAdministrators { get; set; }
    public int BudgetSize { get; set; }
}