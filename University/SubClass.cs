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