namespace Repository;
using University.UCore;
using Logger;
public class UniversityRepository
{
    static UniversityRepository()
    {
        List<Worker> workerRep = new WorkerRepository().ReturnList();
        foreach(var worker in workerRep)
        {
            if (worker.GetType() == typeof(Administrator))
            {
                _workerRep.Add((Administrator)worker);
            }
        }
        _university.Add(new ClassUniversity("VUZ", _workerRep[0],  _workerRep.GetRange(1,1), new Random().Next(10000, 10000000)));
    }

    public UniversityRepository()
    {
        
    }
    
    public void Add(ClassUniversity university, Logger logger)
    {
        try
        {
            _university.Add(university);
            logger.Debug("Worker added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            logger.Error("Worker not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }
    
    public List<ClassUniversity> ReturnList(Logger logger)
    {
        logger.Debug("Return list" + Environment.NewLine);
        return _university;
    }
    
    internal List<ClassUniversity> ReturnList()
    {
        return _university;
    }
    
    private static List<Administrator> _workerRep = new List<Administrator>();
    private static List<ClassUniversity> _university = new List<ClassUniversity>();
}