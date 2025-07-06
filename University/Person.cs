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
    
    private string FirstName;
    private string LastName;
    private string MiddleName;
    private int age;
    private Address address;
    private string MilitaryIDAvailability;
    private bool CriminalRecord;
}