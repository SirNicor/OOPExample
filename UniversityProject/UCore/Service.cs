namespace UCore;
using Logger;
public class Service:Worker
{
    public Service(int salary, 
        Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord) 
        : base(salary,  passport, militaryIdAvailability, criminalRecord)
    {
    }

    public override void DoWork(MyLogger myLogger) 
    {
        
    }
    public override void PrintDerivedClass(MyLogger myLogger)
    {   
        string message;
        message = $"Зарплата - {Salary}" + Environment.NewLine;
        myLogger.Info(message);
    }
}
