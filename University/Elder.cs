namespace University;

public class Elder:Student
{
    public Elder(string firstName, string lastName, int age, Address address, DegreesStudy degrees, int course, Discipline discipline) : base(firstName, lastName, age,
        address, degrees, course, discipline){}
    public Elder(string firstName, string lastName, string middleName, int age, Address address, DegreesStudy degrees, int course, Discipline discipline) : base(firstName, lastName, middleName, age,
        address, degrees, course,  discipline){}
    public Elder(string firstName, string lastName, string middleName, int age, Address address, string millitaryId, bool criminalRecord, DegreesStudy degrees, int course, Discipline discipline) : base(firstName, lastName, middleName, age,
        address, millitaryId, criminalRecord, degrees, course, discipline){}
    
    public bool Protect()
    {
        return ((new Random()).Next(0, _attitudeToTheGroup) < 30 ? false : true);
    }
    public override void VisitingCouple(Elder elder)
    {
        bool skipS = ((new Random()).Next(0, 100) < 10 && !((new Elder()).Protect()));
        if (skipS)
        {
            Console.WriteLine("Вы рискнули пропустить, передав журнал другому студенту, который не захотел вас защитить");
            SkipHours += 2; 
        }
        else
        {
            Console.WriteLine("К сожалению вам необходимо быть на паре");
            Console.WriteLine("Несмотря на все трудности вы были на паре или, хотя бы, так считает преподаватель");
            Console.WriteLine("В журнале отмечены пропустившие студенты, журнал у преподавателя");
            Console.WriteLine("Пара окончена, можно расходиться");
        }
    }
    
    public override void Session()
    {
        Console.WriteLine("Началась сессия, но к вам относятся лучше как к старосте: ");
        for (int i = 0; i < 10; ++i)
        {
            PassingTheExam(MinChances - SkipHours / 2 + СreditScores/CountOfExamsPassed*2 + 20);
        }
        Console.WriteLine("Сессия кончилась, забирайте зачетки и относите в деканат");
    }
    
    protected Elder(){}
    private int _attitudeToTheGroup = (new Random()).Next(40, 100);
}