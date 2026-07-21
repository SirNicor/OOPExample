namespace UCore;
using Logger;
public abstract class Worker : Person
{
    public abstract void DoWork(MyLogger myLogger);

    // protected DateTime StartWork;
    // protected DateTime EndWork;

    public int Salary { get; set; }
}

