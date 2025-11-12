using System.Diagnostics.CodeAnalysis;

namespace UCore;
using Logger;
public abstract class Person
{
    public Person(Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord)
    {
        Passport = passport;
        MilitaryIdAvailability = militaryIdAvailability;
        CriminalRecord = criminalRecord;
    }
    public void PrintInfo(MyLogger myLogger)
    {
        string message = $"Id : {Id}";
        myLogger.Info(message);
        Passport.Print(myLogger);
        message = ($"Военный билет: {MilitaryIdAvailability} и судимость ") + (CriminalRecord?"есть":"нет");
        myLogger.Info(message);
        PrintDerivedClass(myLogger);
    }

    public abstract void PrintDerivedClass(MyLogger myLogger);
    
    protected Person(){}
    public int Id { get; set; }
    public Passport Passport { get; set; }
    public IdMillitary MilitaryIdAvailability { get; set; }
    public bool CriminalRecord { get; set; }
}