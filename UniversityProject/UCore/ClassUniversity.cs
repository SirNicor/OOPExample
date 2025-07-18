namespace University.UCore;

public class ClassUniversity
{
     public ClassUniversity(string nameUniversity, Administrator rector, Administrator[] administrators, int budgetSize)
     {
          NameUniversity = nameUniversity;
          Rector = rector;
          Administrators = administrators;
          BudgetSize = budgetSize;
     }
     
     public readonly string NameUniversity;
     protected Administrator Rector;
     protected Administrator[] Administrators;
     private int BudgetSize;  
}



