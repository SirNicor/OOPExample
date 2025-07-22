using System.Diagnostics.CodeAnalysis;

namespace University.UCore;
using Logger;
public abstract class Person
{
    public Person(Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord)
    {
        Passport = passport;
        MilitaryIdAvailability = militaryIdAvailability;
        CriminalRecord = criminalRecord;
    }
    public void PrintInfo(Logger logger)
    {
        Passport.Print(logger);
        string message = ($"Военный билет: {MilitaryIdAvailability} и судимость ") + (CriminalRecord?"есть":"нет");
        logger.Info(message);
        PrintDerivedClass(logger);
    }

    public abstract void PrintDerivedClass(Logger logger);
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