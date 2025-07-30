using University.UCore;
using Logger;
    
public class WorkerRepository
{
    static WorkerRepository()
    {
        Passport passport;
        Worker worker;
        passport = new Passport(1111, 123456, "Grigory", "Novikov", 
            new DateTime(2000,01,01), new Address("Russia", "Saint Petersburg", "Sredniy Avenue", 35), "1");
        worker = new Teacher(35000,  passport, IdMillitary.InStock, false);
        _workers.Add(worker);
        passport = new Passport(1112, 123457, "Yelisey", "Koltsov", 
            new DateTime(2001,02,03), new Address("Russia", "Saint Petersburg", "Sredniy Avenue", 37), "1");
        worker = new Teacher(45000,  passport, IdMillitary.InStock, false);
        _workers.Add(worker);
        passport = new Passport(1113, 133457, "Mikhail", "Alexandrov", 
            new DateTime(1980,02,03), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 38), "1");
        worker = new Teacher(55000,  passport, IdMillitary.InStock, false);
        _workers.Add(worker);
        passport = new Passport(1122, 123466, "Yaroslav", "Sakharov", 
            new DateTime(1988,12,03), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 38), "1");
        worker = new Teacher(50234,  passport, IdMillitary.InStock, false);
        _workers.Add(worker);
        passport = new Passport(2222, 223466, "Maxim", "Kirillov", 
            new DateTime(1978,12,03), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 45), "1");
        worker = new Administrator(123000,  passport, IdMillitary.InStock, false);
        _workers.Add(worker);
        passport = new Passport(5522, 125766, "Ivan", "Karpov", 
            new DateTime(1977,12,03), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 60), "1");
        worker = new Administrator(90000,  passport, IdMillitary.InStock, false);
        _workers.Add(worker);
    }

    public WorkerRepository()
    {
        
    }
    
    public void Add(Worker worker, Logger.Logger logger)
    {
        try
        {
            _workers.Add(worker);
            logger.Debug("Worker added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            logger.Error("Worker not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }

    public void PrintAll(Logger.Logger logger)
    {
        foreach (Worker worker in _workers)
        {
            worker.PrintInfo(logger);
        }
    }

    public List<Worker> ReturnList(Logger.Logger logger)
    {
        logger.Debug("Return list" + Environment.NewLine);
        return _workers;
    }
    
    internal List<Worker> ReturnList()
    {
        return _workers;
    }
    
    static List<Worker> _workers = new List<Worker>(0);
}