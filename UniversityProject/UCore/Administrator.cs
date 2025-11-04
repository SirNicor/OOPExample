namespace UCore;
using Logger;
public class Administrator : Worker
{
    public Administrator(int salary, 
        Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord) 
        : base(salary,  passport, militaryIdAvailability, criminalRecord)
    {
    }

    public Administrator()
    {
        
    }
    public override void PrintDerivedClass(MyLogger myLogger)
    {
        string message;
        message = $"Зарплата - {Salary}" + Environment.NewLine;
        myLogger.Info(message);
    }

    public override void DoWork(MyLogger myLogger)
    {
        throw new NotImplementedException();
    }
}