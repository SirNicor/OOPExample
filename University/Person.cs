namespace University;

public class Person
{
    public Person(string FirstName, string LastName, int age, Address address)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.age = age;
        this.address = address;
    }
    public Person(string FirstName, string LastName, string middleName, int age, Address address)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        MiddleName = middleName;
        this.age = age;
        this.address = address;
    }
    public Person(string FirstName, string LastName, string middleName, int age, Address address, string MilitaryIDAvailability, bool CriminalRecord)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        MiddleName = middleName;
        this.age = age;
        this.address = address;
        this.MilitaryIDAvailability = MilitaryIDAvailability;
        this.CriminalRecord = CriminalRecord;
    }
    
    protected string FirstName;
    protected string LastName;
    protected string MiddleName;
    protected int age;
    protected Address address;
    protected string MilitaryIDAvailability;
    protected bool CriminalRecord;

    public void Print()
    {
        Console.WriteLine("FullName: " + FirstName + " " + LastName + " " + MiddleName);
        Console.WriteLine("Age: " + age);
        address.Print();
        Console.WriteLine("MilitaryID: " + MilitaryIDAvailability);
        Console.WriteLine("CriminalRecord " + (CriminalRecord?"Yes":"No"));
    }

    public string MilitaryReset
    {
        set { MilitaryIDAvailability = value; }
    }
    
    public bool CriminalRecordReset
    {
        set { CriminalRecord = value; }
    }
}