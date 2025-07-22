namespace University.UCore;
using Logger;
public class Administrator : Worker
{
    public Administrator(int salary, 
        Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord) 
        : base(salary,  passport, militaryIdAvailability, criminalRecord)
    {
    }

    public override void PrintDerivedClass(Logger logger)
    {
        string message;
        message = $"Зарплата - {Salary}" + Environment.NewLine;
        logger.Info(message);
    }

    public override void DoWork()
    {
        throw new NotImplementedException();
    }
}