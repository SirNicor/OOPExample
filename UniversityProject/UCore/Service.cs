namespace UCore;
using Logger;
public class Service:Worker
{
    public Service(int salary, 
        Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord) 
        : base(salary,  passport, militaryIdAvailability, criminalRecord)
    {
    }

    public override void DoWork(Logger logger) 
    {
        
    }
    public override void PrintDerivedClass(Logger logger)
    {   
        string message;
        message = $"Зарплата - {Salary}" + Environment.NewLine;
        logger.Info(message);
    }
}
