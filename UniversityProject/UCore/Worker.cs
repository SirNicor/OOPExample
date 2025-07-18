namespace University.UCore;

public abstract class Worker : Person
{
    public Worker(ClassUniversity university, DateTime startWork, DateTime endWork, int salary)
    {
        University =  university;
        StartWork = startWork;
        EndWork = endWork;
        Salary = salary;
    }
    public abstract void DoWork();
    
    protected ClassUniversity University;
    protected DateTime StartWork;
    protected DateTime EndWork;
    protected int Salary;
    

}

