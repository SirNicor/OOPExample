namespace University;



public class Address
{
    public Address(string country, string city, string street, int houseNumber)
    {
        _country = country;
        _street = street;
        _city = city;
        _houseNumber = houseNumber;
    }
    public void Print()
    {
        Console.WriteLine("FullAddress: " + _country + " " + _city + " " + _street + " " + _houseNumber);
    }
    
    private readonly string _country;
    private readonly string _city;
    private readonly string _street;
    private readonly int _houseNumber;
}