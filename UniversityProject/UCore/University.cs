namespace UCore;

public class University
{
     public University(string nameUniversity, Administrator rector, List<Administrator> administrators, int budgetSize)
     {
          NameUniversity = nameUniversity;
          Rector = rector;
          Administrators = administrators;
          BudgetSize = budgetSize;
     }

     public University()
     {
     }
     public int UniversityId { get; set; }
     public string NameUniversity { get; set; }
     public Administrator Rector { get; set; }
     public List<Administrator> Administrators { get; set; }
     public int BudgetSize { get; set; }
}



