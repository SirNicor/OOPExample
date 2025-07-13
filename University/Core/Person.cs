namespace UCore;

public class Person
{
    public Person(string firstName, string lastName, int age, Address address)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Address = address;
    }
    public Person(string firstName, string lastName, string middleName, int age, Address address)
    {
        FirstName = firstName;
        lastName = lastName;
        MiddleName = middleName;
        Age = age;
        Address = address;
    }
    public Person(string firstName, string lastName, string middleName, int age, Address address, string militaryIdAvailability, bool criminalRecord)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        MiddleName = middleName;
        Age = age;
        Address = address;
        MilitaryIdAvailability = militaryIdAvailability;
        CriminalRecord = criminalRecord;
    }
    public void Print()
    {
        Console.WriteLine("FullName: " + FirstName + " " + LastName + " " + MiddleName);
        Console.WriteLine("Age: " + Age);
        Address.Print();
        Console.WriteLine("MilitaryID: " + MilitaryIdAvailability);
        Console.WriteLine("CriminalRecord " + (CriminalRecord?"Yes":"No"));
    }

    public string MilitaryReset
    {
        set { MilitaryIdAvailability = value; }
    }
    
    public bool CriminalRecordReset
    {
        set { CriminalRecord = value; }
    }
    
    protected Person(){}
    protected readonly string FirstName;
    protected readonly string LastName;
    protected readonly string MiddleName;
    protected int Age;
    protected Address Address;
    protected string MilitaryIdAvailability;
    protected bool CriminalRecord;
}