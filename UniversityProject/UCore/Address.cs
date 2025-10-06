namespace UCore;
using Logger;

public class Address
{
    public Address(string country, string city, string street, int houseNumber)
    {
        Country = country;
        Street = street;
        City = city;
        HouseNumber = houseNumber;
    }
    public Address(){}
    public void Print(MyLogger myLogger)
    {
        myLogger.Info("FullAddress: " + Country + " " + City + " " + Street + " " + HouseNumber);
    }

    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int HouseNumber { get; set; }
}