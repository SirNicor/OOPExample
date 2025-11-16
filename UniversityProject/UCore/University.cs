namespace UCore;

public class University
{
     public University(string nameUniversity, List<Administrator> administrators, int budgetSize)
     {
          NameUniversity = nameUniversity;
          Administrators = administrators;
          BudgetSize = budgetSize;
     }

     public University()
     {
     }
     public long UniversityId { get; set; }
     public string NameUniversity { get; set; }
     public List<Administrator> Administrators { get; set; }
     public int BudgetSize { get; set; }
}



