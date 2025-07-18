namespace University.UCore;

public abstract class Person
{
    public Person(Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord)
    {
        Passport = passport;
        MilitaryIdAvailability = militaryIdAvailability;
        CriminalRecord = criminalRecord;
    }
    public void Print()
    {
        
    }

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