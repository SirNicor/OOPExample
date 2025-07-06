namespace University;

public class Student:Person
{
    public Student(string FirstName, string LastName, int age, Address address, DegreesStudy degrees, int course, int CreditScores, int SkipHours) : base(FirstName, LastName, age,
        address)
    {
        Degrees =  degrees;
        this.course = CheckMethods.checkDegress(course, degrees);
        СreditScores = CreditScores;
        this.SkipHours = SkipHours;
    }

    public Student(string FirstName, string LastName, string MiddleName, int age, Address address, DegreesStudy degrees,
        int course, int CreditScores, int SkipHours) : base(FirstName, LastName, MiddleName, age,
        address)
    {
        Degrees =  degrees;
        course = CheckMethods.checkDegress(course, degrees);
        СreditScores = CreditScores;
        this.SkipHours = SkipHours;
    }

    public Student(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId,
        bool CriminalRecord, DegreesStudy degrees, int course, int CreditScores, int SkipHours) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord)
    {
        Degrees =  degrees;
        course = CheckMethods.checkDegress(course, degrees);
        СreditScores = CreditScores;
        this.SkipHours = SkipHours;
    }

    protected string GroupCipher;
    protected int СreditScores;
    protected int SkipHours;
    protected int course;
    protected DegreesStudy Degrees;
}

public class Elder:Student
{
    public Elder(string FirstName, string LastName, int age, Address address, DegreesStudy degrees, int course, int CreditScores, int SkipHours) : base(FirstName, LastName, age,
        address, degrees, course, CreditScores, SkipHours){}
    public Elder(string FirstName, string LastName, string MiddleName, int age, Address address, DegreesStudy degrees, int course, int CreditScores, int SkipHours) : base(FirstName, LastName, MiddleName, age,
        address, degrees, course, CreditScores, SkipHours){}
    public Elder(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId, bool CriminalRecord, DegreesStudy degrees, int course, int CreditScores, int SkipHours) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord, degrees, course, CreditScores, SkipHours){}
}

public class Proforg:Student
{
    public Proforg(string FirstName, string LastName, int age, Address address, DegreesStudy degrees, int course, int CreditScores, int SkipHours) : base(FirstName, LastName, age,
        address, degrees, course, CreditScores, SkipHours){}
    public Proforg(string FirstName, string LastName, string MiddleName, int age, Address address, DegreesStudy degrees, int course, int CreditScores, int SkipHours) : base(FirstName, LastName, MiddleName, age,
        address, degrees, course, CreditScores, SkipHours){}
    public Proforg(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId, bool CriminalRecord, DegreesStudy degrees, int course, int CreditScores, int SkipHours) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord, degrees, course, CreditScores, SkipHours){}
}
