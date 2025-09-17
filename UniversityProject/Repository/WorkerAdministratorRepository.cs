namespace Repository;
using UCore;
using Logger;

public class WorkerAdministratorRepository : IWorkerAdministratorRepository
{
    public WorkerAdministratorRepository(MyLogger myLogger)
    {
        try
        {
            Passport passport;
            Administrator worker;
            passport = new Passport(2222, 223466, "Maxim", "Kirillov", 
                new DateTime(1978,12,03), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 45), "1");
            worker = new Administrator(123000,  passport, IdMillitary.InStock, false);
            _workersAdministrator.Add(worker);
            passport = new Passport(5522, 125766, "Ivan", "Karpov", 
                new DateTime(1977,12,03), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 60), "1");
            worker = new Administrator(90000,  passport, IdMillitary.InStock, false);
            _workersAdministrator.Add(worker);
            passport = new Passport(2222, 223466, "ad2", "ad2", 
                new DateTime(1978,12,03), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 45), "1");
            worker = new Administrator(123000,  passport, IdMillitary.InStock, false);
            _workersAdministrator.Add(worker);
            passport = new Passport(5522, 125766, "ad3", "ad3", 
                new DateTime(1977,12,03), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 60), "1");
            worker = new Administrator(90000,  passport, IdMillitary.InStock, false);
            _workersAdministrator.Add(worker);
            Random random = new Random();
            for (int i = 4; i < 9; i++)
            {
                passport = new Passport(random.Next(1000, 9999), random.Next(100000, 999999), $"ad{i}", $"ad{i}", 
                    new DateTime(random.Next(1950, 2005),random.Next(1, 12),random.Next(1, 30)), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 45), "1");
                worker = new Administrator(random.Next(80000, 1000000),  passport, IdMillitary.InStock, false);
                _workersAdministrator.Add(worker);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    
    public void AddAdministrator(Administrator worker, MyLogger myLogger)
    {
        try
        {
            _workersAdministrator.Add(worker);
            myLogger.Debug("Worker added" + Environment.NewLine);
            throw new Exception("Worker not added");
        }
        catch(Exception exception)
        {
            myLogger.Error("Worker not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }
    
    public void PrintAll(MyLogger myLogger)
    {
        foreach (Worker worker in _workersAdministrator)
        {
            worker.PrintInfo(myLogger);
        }
    }
    
    public List<Administrator> ReturnListAdministrator(MyLogger myLogger)
    {
        myLogger.Debug("Return list" + Environment.NewLine);
        return _workersAdministrator;
    }
    
    private static List<Administrator> _workersAdministrator = new List<Administrator>();
}