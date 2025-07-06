namespace University;

public class Student:Person
{
    public Student(string FirstName, string LastName, int age, Address address) : base(FirstName, LastName, age,
        address){}
    public Student(string FirstName, string LastName, string MiddleName, int age, Address address) : base(FirstName, LastName, MiddleName, age,
        address){}
    public Student(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId, bool CriminalRecord) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord){}
    
}

public class Elder:Student
{
    public Elder(string FirstName, string LastName, int age, Address address) : base(FirstName, LastName, age,
        address){}
    public Elder(string FirstName, string LastName, string MiddleName, int age, Address address) : base(FirstName, LastName, MiddleName, age,
        address){}
    public Elder(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId, bool CriminalRecord) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord){}
}

public class Proforg:Student
{
    public Proforg(string FirstName, string LastName, int age, Address address) : base(FirstName, LastName, age,
        address){}
    public Proforg(string FirstName, string LastName, string MiddleName, int age, Address address) : base(FirstName, LastName, MiddleName, age,
        address){}
    public Proforg(string FirstName, string LastName, string MiddleName, int age, Address address, string MillitaryId, bool CriminalRecord) : base(FirstName, LastName, MiddleName, age,
        address, MillitaryId, CriminalRecord){}
}
