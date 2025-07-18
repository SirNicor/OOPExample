namespace University.UCore;
using Logger;
public class Teacher:Worker
{
    public Teacher(ClassUniversity university, DateTime startWork, DateTime endWork, int salary) : base(university, startWork, endWork, salary)
    {
    }

    public override void DoWork()
    {
        
    }

    public override void PrintDerivedClass(Logger logger)
    {
        string message = $"Начало работы {StartWork} и конец работы {EndWork}" + Environment.NewLine;
        message += $"Зарплата - {Salary}";
        logger.Log(LevelLoger.INFO, message);
    }
}