using System.Net.Security;

namespace University;

public class Student:Person
{
    protected const int MinChances = 200;
    public Student(string FirstName, string LastName, int age, Address address, DegreesStudy degrees, int course, Discipline discipline) : base(FirstName, LastName, age,
        address)
    {
        Degrees =  degrees;
        this.course = CheckMethods.checkDegress(course, degrees);
        SkipHours = 0;
        CountOfExamsPassed = 0;
        СreditScores = 0;
        Cipher = discipline.ReturnGroupCipher();
    }
    
    public Student(string FirstName, string LastName, string MiddleName, int age, Address address, DegreesStudy degrees,
        int course, Discipline discipline) : base(FirstName, LastName, MiddleName, age,
        address)
    {
        Degrees =  degrees;
        this.course = CheckMethods.checkDegress(course, degrees);
        SkipHours = 0;
        CountOfExamsPassed = 0;
        СreditScores = 0;
        Cipher = discipline.ReturnGroupCipher();
    }
    public Student(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId,
        bool CriminalRecord, DegreesStudy degrees, int course, Discipline discipline) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord)
    {
        Degrees =  degrees;
        this.course = CheckMethods.checkDegress(course, degrees);
        SkipHours = 0;
        CountOfExamsPassed = 0;
        СreditScores = 0;
        Cipher = discipline.ReturnGroupCipher();
    }
    protected Student(){}
    
    protected string GroupCipher;
    protected int СreditScores;
    protected int CountOfExamsPassed;
    protected int SkipHours;
    protected int course;
    protected DegreesStudy Degrees;
    protected string Cipher;
    
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

    private bool Skip(Elder elder)
    {
        bool protect = false;
        if (this.Cipher == elder.Cipher && this.course == elder.course)
            protect = elder.Protect();
        else
            Console.WriteLine("Вы обратились не к своему старосте. ");
        if (new Random().Next(0, 100) < 40 || protect)
            return false;
        else
            return true;
    }

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

    public virtual void session()
    {
        Console.WriteLine("Началась сессия: ");
        for (int i = 0; i < 10; ++i)
            PassingTheExam(MinChances - SkipHours / 2 + СreditScores/CountOfExamsPassed*2);
        Console.WriteLine("Сессия кончилась, отдавайте зачетную книжку старосте или лично несите ее в деканат на подпись.");
        Console.WriteLine("Конечный результат на сессии: " + TotalScore);
    }

    public double TotalScore {
        get {return ((double)СreditScores/CountOfExamsPassed)/5*100;}
    }
}

public class Elder:Student
{
    public Elder(string FirstName, string LastName, int age, Address address, DegreesStudy degrees, int course, Discipline discipline) : base(FirstName, LastName, age,
        address, degrees, course, discipline){}
    public Elder(string FirstName, string LastName, string MiddleName, int age, Address address, DegreesStudy degrees, int course, Discipline discipline) : base(FirstName, LastName, MiddleName, age,
        address, degrees, course,  discipline){}
    public Elder(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId, bool CriminalRecord, DegreesStudy degrees, int course, Discipline discipline) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord, degrees, course, discipline){}

    protected Elder(){}
    private int AttitudeToTheGroup = (new Random()).Next(40, 100);
    public bool Protect()
    {
        return ((new Random()).Next(0, AttitudeToTheGroup) < 30 ? false : true);
    }
    public override void VisitingCouple(Elder elder)
    {
        bool SkipS = ((new Random()).Next(0, 100) < 10 && !((new Elder()).Protect()));
        if (SkipS)
        {
            Console.WriteLine("Вы рискнули пропустить, передав журнал другому студенту, который не захотел вас защитить");
            SkipHours += 2; 
        }
        else
        {
            Console.WriteLine("К сожалению вам необходимо быть на паре");
            Console.WriteLine("Несмотря на все трудности вы были на паре или, хотя бы, так считает преподаватель");
            Console.WriteLine("В журнале отмечены недбросовестные студенты, журнал у преподавателя");
            Console.WriteLine("Пара окончена, можно расходиться");
        }
    }
    public override void session()
    {
        Console.WriteLine("Началась сессия, но к вам относятся лучше как к старосте: ");
        for (int i = 0; i < 10; ++i)
            PassingTheExam(MinChances - SkipHours / 2 + СreditScores/CountOfExamsPassed*2 + 20);
        Console.WriteLine("Сессия кончилась, забирайте зачетки и относите в деканат");
    }
}

/*
public class Proforg:Student
{
    public Proforg(string FirstName, string LastName, int age, Address address, DegreesStudy degrees, int course) : base(FirstName, LastName, age,
        address, degrees, course){}
    public Proforg(string FirstName, string LastName, string MiddleName, int age, Address address, DegreesStudy degrees, int course) : base(FirstName, LastName, MiddleName, age,
        address, degrees, course){}
    public Proforg(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId, bool CriminalRecord, DegreesStudy degrees, int course) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord, degrees, course){}
}
*/
