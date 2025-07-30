using System.Net.Security;
using Logger;

namespace University.UCore;
using Logger;
public class Student:Person
{
    protected const int MinChances = 200;
    public Student(Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord, int course, bool accomodationDormitories, DegreesStudy degreesStudy) :
        base(passport, militaryIdAvailability, criminalRecord)
    {
        _course = CheckMethods.CheckDegress(course, degreesStudy);
        _skipHours = 0;
        _countOfExamsPassed = 0;
        _creditScores = 0;
        _accomodationDormitories =  accomodationDormitories;
    }
    /*public virtual void VisitingCouple(Elder elder)
    {
        if (Skip(elder))
        {
            SkipHours+=2;
            Console.WriteLine($"Вас, {FirstName} {LastName} {MiddleName}, не видели на паре");
        }
        else    
        {
            Console.WriteLine($"Несмотря на все трудности вы, {FirstName} {LastName} {MiddleName}, были на паре или, хотя бы, так считает преподаватель");
            Console.WriteLine("Пара окончена, можно расходиться");
        }
        Console.WriteLine();
    }
    public virtual void Session()
    {
        Console.WriteLine("Началась сессия: ");
        for (int i = 0; i < 10; ++i)
        {
            PassingTheExam(MinChances - SkipHours / 2 + (int)TotalScore*2);
        }
        Console.WriteLine("Сессия кончилась, отдавайте зачетную книжку старосте или лично несите ее в деканат на подпись.");
        Console.WriteLine("Конечный результат сессии: " + TotalScore);
    }*/
    public double TotalScore {
        get
        {
            if (_countOfExamsPassed == 0)
            {
                return 0;
            }
            else
            {
                return ((double)_creditScores/_countOfExamsPassed)/5*100;
            }
        }
    }

    public override void PrintDerivedClass(Logger logger)
    {
        string message = $"Course: {_course}" + Environment.NewLine;
        message += $"Общий балл ={_creditScores} и количество сданных экзаменов = {_countOfExamsPassed} и общий балл = {TotalScore}" + Environment.NewLine;
        message += "Расположен ли в общежитии " + (_accomodationDormitories ? "Да" : "Нет");
        logger.Info(message);
    }

    protected Student(){}
    private int _creditScores;
    private int _countOfExamsPassed;
    private int _skipHours;
    private int _course;
    private bool _accomodationDormitories;
    /*protected void PassingTheExam(int chances)
    {
        if ((new Random()).Next(0, chances) > 150)
        {
            СreditScores += 5;
            Console.WriteLine("Вы сдали на отлично");
        }
        else if ((new Random()).Next(0, chances) > 100)
        {
            СreditScores += 4;
            Console.WriteLine("Вы сдали на хорошо");
        }
        else if ((new Random()).Next(0, chances) > 50)
        {
            СreditScores += 3;
            Console.WriteLine("Вы сдали на удовлетворительно");
        }
        else
        {
            СreditScores += 2;
            Console.WriteLine("Вы не сдали");
        }
        ++CountOfExamsPassed;
    }
    
    private bool Skip(Elder elder)
    {
        bool protect = false;
        if (this.Cipher == elder.Cipher && Course == elder.Course)
        {
            protect = elder.Protect();
        }
        else
        {
            Console.WriteLine("Вы обратились не к своему старосте. ");
        }
        if (new Random().Next(0, 100) < 40 || protect)
        {
            return false;
        }
        return true;
    }*/
}




