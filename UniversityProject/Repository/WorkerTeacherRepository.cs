namespace Repository;
using UCore;
using Logger;

public class WorkerTeacherRepository : IWorkerTeacherRepository
{
    public WorkerTeacherRepository(MyLogger myLogger, IDisciplineRepository disciplineRepository)
    {
        try
        {
            Passport passport;
            Teacher worker;
            List<Discipline> disciplines;   
            _disciplines = disciplineRepository;
            List<Discipline> allDisciplines = _disciplines.ReturnList(myLogger);
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
    
    
    
    public void Add(Teacher worker, MyLogger myLogger) 
    {
        try
        {
            _workersTeachers.Add(worker);
            myLogger.Debug("Worker added" + Environment.NewLine);
            throw new Exception("Worker not added");
        }
        catch(Exception exception)
        {
            myLogger.Error("Worker not added, The information is incomplete " + Environment.NewLine, exception);
            throw exception;
        }
    }
    
    

    public void PrintAll(MyLogger myLogger)
    {
        foreach (Worker worker in _workersTeachers)
        {
            worker.PrintInfo(myLogger);
        }
    }

    public List<Teacher> ReturnListTeachers(MyLogger myLogger)
    {
        myLogger.Debug("Return list" + Environment.NewLine);
        return _workersTeachers;
    }
    
    private static List<Teacher> _workersTeachers = new List<Teacher>();
    static IDisciplineRepository _disciplines;
}