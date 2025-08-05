namespace UCore;

public class ClassUniversity
{
     public ClassUniversity(string nameUniversity, Administrator rector, List<Administrator> administrators, int budgetSize)
     {
          NameUniversity = nameUniversity;
          Rector = rector;
          Administrators = administrators;
          BudgetSize = budgetSize;
     }
     
     public readonly string NameUniversity;
     protected Administrator Rector;
     protected List<Administrator> Administrators;
     private int BudgetSize;  
}



