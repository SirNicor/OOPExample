using System.Net.Security;

namespace University;

public class Student:Person
{
    protected const int MinChances = 200;
    public Student(string firstName, string lastName, int age, Address address, DegreesStudy degrees, int course, Discipline discipline) : base(firstName, lastName, age,
        address)
    {
        Degrees =  degrees;
        Course = CheckMethods.CheckDegress(course, degrees);
        SkipHours = 0;
        CountOfExamsPassed = 0;
        СreditScores = 0;
        Cipher = discipline.ReturnGroupCipher();
    }
    
    public Student(string firstName, string lastName, string middleName, int age, Address address, DegreesStudy degrees,
        int course, Discipline discipline) : base(firstName, lastName, middleName, age,
        address)
    {
        Degrees =  degrees;
        Course = CheckMethods.CheckDegress(course, degrees);
        SkipHours = 0;
        CountOfExamsPassed = 0;
        СreditScores = 0;
        Cipher = discipline.ReturnGroupCipher();
    }
    public Student(string firstName, string lastName, string middleName, int age, Address address, string millitaryId,
        bool criminalRecord, DegreesStudy degrees, int course, Discipline discipline) : base(firstName, lastName, middleName, age,
        address, millitaryId, criminalRecord)
    {
        Degrees =  degrees;
        Course = CheckMethods.CheckDegress(course, degrees);
        SkipHours = 0;
        CountOfExamsPassed = 0;
        СreditScores = 0;
        Cipher = discipline.ReturnGroupCipher();
    }
    public virtual void VisitingCouple(Elder elder)
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
    }
    public double TotalScore {
        get
        {
            if (CountOfExamsPassed == 0)
            {
                return 0;
            }
            else
            {
                return ((double)СreditScores/CountOfExamsPassed)/5*100;
            }
        }
    }
    protected Student(){}
    protected string GroupCipher;
    protected int СreditScores;
    protected int CountOfExamsPassed;
    protected int SkipHours;
    protected int Course;
    protected DegreesStudy Degrees;
    protected string Cipher;
    protected void PassingTheExam(int chances)
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
    }

    
}




