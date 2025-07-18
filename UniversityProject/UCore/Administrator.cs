namespace University.UCore;
using Logger;
public class Administrator : Worker
{
    public Administrator(ClassUniversity university, DateTime startWork, DateTime endWork, int salary) : base(university, startWork, endWork, salary)
    {
    }

    public override void PrintDerivedClass(Logger logger)
    {
        string message = $"Начало работы {StartWork} и конец работы {EndWork}" + Environment.NewLine;
        message += $"Зарплата - {Salary}";
        logger.Log(LevelLoger.INFO, message);
    }

    public override void DoWork()
    {
        throw new NotImplementedException();
    }
}