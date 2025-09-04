using System.Security.Cryptography;

namespace UCore;
using Logger;
public class Passport
{
    public Passport(int serial, int number, string firstName, string lastName, string middleName, DateTime birthDate, Address address, string placeRepeipt)
    {
        _serial = serial;
        _number = number;
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _birthDate = birthDate;
        _address = address;
        _placeRepeipt = placeRepeipt;
    }
    public Passport(int serial, int number, string firstName, string lastName, DateTime birthDate, Address address, string placeRepeipt)
    {
        _serial = serial;
        _number = number;
        _firstName = firstName;
        _lastName = lastName;
        _birthDate = birthDate;
        _address = address;
        _placeRepeipt = placeRepeipt;
    }

    public void Print(MyLogger myLogger)
    {
        string message = $"FullName: {_firstName} {_lastName} {_middleName}, BirthDate: {_birthDate}";
        message += Environment.NewLine + $"Serial: {_serial} Number: {_number} issued by whom: {_placeRepeipt}";
        myLogger.Info(message);
        _address.Print(myLogger);
    }
    
    private int _serial;
    private int _number;
    private string _firstName;
    private string _lastName;
    private string _middleName;
    private string _placeRepeipt;
    private Address _address;
    private DateTime _birthDate;
}