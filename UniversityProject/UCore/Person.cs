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
        Passport.Print(myLogger);
        string message = ($"Военный билет: {MilitaryIdAvailability} и судимость ") + (CriminalRecord?"есть":"нет");
        myLogger.Info(message);
        PrintDerivedClass(myLogger);
    }

    public abstract void PrintDerivedClass(MyLogger myLogger);
    public IdMillitary MilitaryReset
    {
        set { MilitaryIdAvailability = value; }
    }
    
    public bool CriminalRecordReset
    {
        set { CriminalRecord = value; }
    }
    
    protected Person(){}
    protected Passport Passport;
    protected IdMillitary MilitaryIdAvailability;
    protected bool CriminalRecord;
}