namespace UCore;
using Logger;
public abstract class Worker : Person
{
    public Worker(int salary,
    Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord):base(passport, militaryIdAvailability, criminalRecord)
    {
        Salary = salary;
    }
    public abstract void DoWork(Logger logger);

    // protected DateTime StartWork;
    // protected DateTime EndWork;
    protected int Salary;

    public int ReturnSalary
    {
        get { return Salary;}
        set { Salary = value; }
    }
}

