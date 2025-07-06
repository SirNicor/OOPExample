using System.Net.Security;

namespace University;

public class Student:Person
{
    public Student(string FirstName, string LastName, int age, Address address, DegreesStudy degrees, int course) : base(FirstName, LastName, age,
        address)
    {
        Degrees =  degrees;
        this.course = CheckMethods.checkDegress(course, degrees);
        SkipHours = 0;
        СreditScores = 100;
    }
    public Student(string FirstName, string LastName, string MiddleName, int age, Address address, DegreesStudy degrees,
        int course) : base(FirstName, LastName, MiddleName, age,
        address)
    {
        Degrees =  degrees;
        course = CheckMethods.checkDegress(course, degrees);
        SkipHours = 0;
        СreditScores = 100;
    }
    public Student(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId,
        bool CriminalRecord, DegreesStudy degrees, int course) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord)
    {
        Degrees =  degrees;
        course = CheckMethods.checkDegress(course, degrees);
        SkipHours = 0;
        СreditScores = 100;
    }
    protected Student(){}
    
    protected string GroupCipher;
    protected int СreditScores;
    protected int SkipHours;
    protected int course;
    protected DegreesStudy Degrees;

    public virtual void VisitingCouple(Elder elder)
    {
        if (Skip(elder))
        {
            SkipHours+=2;
            Console.WriteLine("Вас не видели на паре");
        }
        else
        {
            Console.WriteLine("Несмотря на все трудности вы были на паре или, хотя бы, так считает преподаватель");
        }
        Console.WriteLine("Пара окончена, можно расходиться");
    }

    private bool Skip(Elder elder)
    {
        if (new Random().Next(0, 100) < 40 && !elder.Protect())
            return false;
        else
            return true;
    }
}

public class Elder:Student
{
    public Elder(string FirstName, string LastName, int age, Address address, DegreesStudy degrees, int course) : base(FirstName, LastName, age,
        address, degrees, course){}
    public Elder(string FirstName, string LastName, string MiddleName, int age, Address address, DegreesStudy degrees, int course) : base(FirstName, LastName, MiddleName, age,
        address, degrees, course){}
    public Elder(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId, bool CriminalRecord, DegreesStudy degrees, int course, int CreditScores, int SkipHours) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord, degrees, course){}

    protected Elder(){}
    private int AttitudeToTheGroup = (new Random()).Next(40, 100);
    public bool Protect()
    {
        return ((new Random()).Next(0, AttitudeToTheGroup) < 30 ? true : false);
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
}

public class Proforg:Student
{
    public Proforg(string FirstName, string LastName, int age, Address address, DegreesStudy degrees, int course) : base(FirstName, LastName, age,
        address, degrees, course){}
    public Proforg(string FirstName, string LastName, string MiddleName, int age, Address address, DegreesStudy degrees, int course) : base(FirstName, LastName, MiddleName, age,
        address, degrees, course){}
    public Proforg(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId, bool CriminalRecord, DegreesStudy degrees, int course) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord, degrees, course){}
}
