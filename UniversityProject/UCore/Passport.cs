using System.Security.Cryptography;

namespace UCore;
using Logger;
public class Passport
{
    public Passport(int serial, int number, string firstName, string lastName, string middleName, DateTime birthData, Address address, string placeReceipt)
    {
        Serial = serial;
        Number = number;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        BirthData = birthData;
        Address = address;
        PlaceReceipt = placeReceipt;
    }
    public Passport(int serial, int number, string firstName, string lastName, DateTime birthData, Address address, string placeReceipt)
    {
        Serial = serial;
        Number = number;
        FirstName = firstName;
        LastName = lastName;
        BirthData = birthData;
        Address = address;
        PlaceReceipt = placeReceipt;
    }

    public Passport() { }
    public void Print(MyLogger myLogger)
    {
        string message = $"Id: {PassportId}, FullName: {FirstName} {LastName} {MiddleName}, BirthDate: {BirthData}";
        message += Environment.NewLine + $"Serial: {Serial} Number: {Number} issued by whom: {PlaceReceipt}";
        myLogger.Info(message);
        Address.Print(myLogger);
    }
    public long PassportId { get; set; }
    public int Serial { get; set; }
    public int Number { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthData { get; set; }
    public Address Address { get; set; }
    public string PlaceReceipt { get; set; }
}