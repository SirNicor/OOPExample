namespace Repository;
using UCore;
using Logger;

public class WorkerRepository
{
    public WorkerRepository(Logger logger, DisciplineRepository disciplineRepository)
    {
        try
        {
            Passport passport;
            Teacher worker;
            List<Discipline> disciplines;   
            _disciplines = disciplineRepository;
            List<Discipline> allDisciplines = _disciplines.ReturnList();
            disciplines = new List<Discipline>(){allDisciplines[0], allDisciplines[1]};
            passport = new Passport(1111, 123456, "Grigory", "Novikov", 
                new DateTime(2000,01,01), new Address("Russia", "Saint Petersburg", "Sredniy Avenue", 35), "1");
            worker = new Teacher(35000,  passport, IdMillitary.InStock, false, disciplines);
            _workersTeachers.Add(worker);
            disciplines = new List<Discipline>(){allDisciplines[3], allDisciplines[4], allDisciplines[5]};
            passport = new Passport(1112, 123457, "Yelisey", "Koltsov", 
                new DateTime(2001,02,03), new Address("Russia", "Saint Petersburg", "Sredniy Avenue", 37), "1");
            worker = new Teacher(45000,  passport, IdMillitary.InStock, false,  disciplines);
            _workersTeachers.Add(worker);
            disciplines = new List<Discipline>(){allDisciplines[6], allDisciplines[7], allDisciplines[8], allDisciplines[9]};
            passport = new Passport(1113, 133457, "Mikhail", "Alexandrov", 
                new DateTime(1980,02,03), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 38), "1");
            worker = new Teacher(55000,  passport, IdMillitary.InStock, false,  disciplines);
            _workersTeachers.Add(worker);
            disciplines = new List<Discipline>(){allDisciplines[10], allDisciplines[1], allDisciplines[3]};
            passport = new Passport(1122, 123466, "Yaroslav", "Sakharov", 
                new DateTime(1988,12,03), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 38), "1");
            worker = new Teacher(50234,  passport, IdMillitary.InStock, false, disciplines);
            _workersTeachers.Add(worker);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    
    public WorkerRepository(Logger logger)
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
    
    public void AddTeacher(Teacher worker, Logger logger)
    {
        try
        {
            _workersTeachers.Add(worker);
            logger.Debug("Worker added" + Environment.NewLine);
            throw new Exception("Worker not added");
        }
        catch(Exception exception)
        {
            logger.Error("Worker not added, The information is incomplete " + Environment.NewLine, exception);
            throw exception;
        }
    }
    
    public void AddAdministrator(Administrator worker, Logger logger)
    {
        try
        {
            _workersAdministrator.Add(worker);
            logger.Debug("Worker added" + Environment.NewLine);
            throw new Exception("Worker not added");
        }
        catch(Exception exception)
        {
            logger.Error("Worker not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }

    public void PrintAll(Logger logger)
    {
        foreach (Worker worker in _workersTeachers)
        {
            worker.PrintInfo(logger);
        }

        foreach (Worker worker in _workersAdministrator)
        {
            worker.PrintInfo(logger);
        }
    }

    public List<Teacher> ReturnListTeachers(Logger logger)
    {
        logger.Debug("Return list" + Environment.NewLine);
        return _workersTeachers;
    }
    
    public List<Administrator> ReturnListAdministrator(Logger logger)
    {
        logger.Debug("Return list" + Environment.NewLine);
        return _workersAdministrator;
    }
    
    private static List<Teacher> _workersTeachers = new List<Teacher>();
    private static List<Administrator> _workersAdministrator = new List<Administrator>();
    private static List<Worker> _workersService = new List<Worker>();
    static DisciplineRepository _disciplines;
}