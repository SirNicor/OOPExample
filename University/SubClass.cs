namespace University;


public enum DegreesStudy
{
    bachelor = 4,
    specialization = 5,
    master = 6,
    doctoral = 10,
}
public class Address
{
    public Address(string country, string city, string street, int houseNumber)
    {
        Country = country;
        Street = street;
        City = city;
        HouseNumber = houseNumber;
    }
    private string Country;
    private string City;
    private string Street;
    private int HouseNumber;


    public void Print()
    {
        Console.WriteLine("FullAddress: " + Country + " " + City + " " + Street + " " + HouseNumber);
    }
}

public static class CheckMethods
{
    public static int checkDegress(int course, DegreesStudy degrees)
    {
        if(course>(int)degrees)
            return (int)degrees;
        else if (course < 1)
            return 1;
        else return course;
    }
}