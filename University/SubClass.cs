namespace University;

public class Address
{
    public Address(string country, string street, string city, string houseNumber)
    {
        Country = country;
        Street = street;
        City = city;
        HouseNumber = houseNumber;
    }
    private string Country;
    private string City;
    private string Street;
    private string HouseNumber;


    public void Print()
    {
        Console.WriteLine("FullAddress: " + Country + " " + City + " " + Street + " " + HouseNumber);
    }
}

class Group
{
    private Faculty faculty;
    private Department department;
    private Discipline discipline;

    public string ReturnGroupCipher()
    {
        return ClassUniversity.NameUniversity + faculty.ReturnNameFaculty + department.ReturnNameDepatment + discipline.ReturnNameDiscipline;
    }
}