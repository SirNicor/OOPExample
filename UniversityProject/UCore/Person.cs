using System.Diagnostics.CodeAnalysis;

namespace UCore;
using Logger;
public abstract class Person
{
    public void PrintInfo(MyLogger myLogger)
    {
        string message = $"";
        myLogger.Info(message);
        Passport.Print(myLogger);
        message = ($"Военный билет: {MilitaryIdAvailability} и судимость ") + (CriminalRecord?"есть":"нет");
        myLogger.Info(message);
        PrintDerivedClass(myLogger);
    }

    public abstract void PrintDerivedClass(MyLogger myLogger);
    
    protected Person(){}
    public long Id { get; set; }
    public long PassportId { get; set; }
    public int MillitaryId { get; set; }
    public Passport Passport { get; set; }
    public IdMillitary MilitaryIdAvailability { get; set; }
    public IdMillitaryClass Millitary { get; set; } = null!;
    public bool CriminalRecord { get; set; }
}