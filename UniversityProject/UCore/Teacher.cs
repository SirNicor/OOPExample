namespace UCore;
using Logger;
using System.Linq;
public class Teacher:Worker
{
    public Teacher(int salary, 
        Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord, List<Discipline> discipline) 
        : base(salary,  passport, militaryIdAvailability, criminalRecord)
    {
        _discipline = discipline;
    }

    public override void DoWork(MyLogger myLogger)
    {
        foreach (var discipline in _discipline)
        {
            discipline.Directions.SelectMany(T => T.Students);
            foreach (var direction in discipline.Directions)
            {
                foreach (var student in direction.Students)
                {
                    Random random = new Random();
                    if (random.Next(1, 100) < 20)
                    {
                        student.SkipHours = 1;
                        student.CreditScores = -1;
                    }
                    else
                    {
                        student.CreditScores = 1;
                    }
                }
            }
        }
        myLogger.Info("DoWork Complete" + Environment.NewLine);
    }
    
    public void DoSession(MyLogger myLogger)
    {
        foreach (var discipline in _discipline)
        {
            foreach (var direction in discipline.Directions)
            {
                foreach (var student in direction.Students)
                {
                    Random random = new Random();
                    student.CreditScores = random.Next(20, 100);
                    student.CountOfExamsPassed = 1;
                }
            }
        }
        myLogger.Info("DoSession Complete" + Environment.NewLine);
    }
    public override void PrintDerivedClass(MyLogger myLogger)
    {
        string message;
        message = $"Зарплата - {Salary}" + Environment.NewLine;
        myLogger.Info(message);
    }
    
    private List<Discipline> _discipline;
}