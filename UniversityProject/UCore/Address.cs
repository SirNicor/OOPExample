namespace UCore;
using Logger;

public class Address
{
    public Address(string country, string city, string street, string houseNumber)
    {
        Country = country;
        Street = street;
        City = city;
        HouseNumber = houseNumber;
    }
    public Address(){}
    public void Print(MyLogger myLogger)
    {
        myLogger.Info($"Id: {AddressId}, FullAddress: " + Country + " " + City + " " + Street + " " + HouseNumber);
    }
    public long AddressId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
}