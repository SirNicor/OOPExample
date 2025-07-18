namespace University.UCore;
using Logger;
public class Service:Worker
{
    public void DoWork()
    {
        
    }
    public override void PrintDerivedClass(Logger logger)
    {
        string message = $"Начало работы {StartWork} и конец работы {EndWork}" + Environment.NewLine;
        message += $"Зарплата - {Salary}";
        logger.Log(LevelLoger.INFO, message);
    }
}
