namespace University.UCore;
using Logger;
public class Security:Worker
{
    public Security(int salary, 
        Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord) 
        : base(salary,  passport, militaryIdAvailability, criminalRecord)
    {
    }

    public override void DoWork()
    {
        
    }  
    
    public override void PrintDerivedClass(Logger logger)
    {
        string message;
        message = $"Зарплата - {Salary}" + Environment.NewLine;
        logger.Info(message);
    }
}