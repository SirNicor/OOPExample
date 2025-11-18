namespace UCore;
using Logger;
using System.Linq;
public class Teacher:Worker
{
    public Teacher(int salary, 
        Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord) 
        : base(salary,  passport, militaryIdAvailability, criminalRecord)
    {
        
    }

    public Teacher()
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