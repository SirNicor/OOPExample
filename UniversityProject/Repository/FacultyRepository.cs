namespace Repository;
using University.UCore;
using Logger;

public class FacultyRepository
{
    static FacultyRepository()
    {
        _universityRepository = new UniversityRepository();
        List<Worker> workerRep = new WorkerRepository().ReturnList();
        UniversityRepository rep = new UniversityRepository();
        foreach (var worker in workerRep)
        {
            if (worker.GetType() == typeof(Administrator))
            {
                _workerRep.Add((Administrator)worker);
            }
        } 
        _faculties.Add(new Faculty("FITU", _workerRep[2],  _workerRep[3], _workerRep.GetRange(4, 1), _universityRepository.ReturnList()[0]));
    }

    public FacultyRepository()
    {
        
    }
    
    public void Add(Faculty faculty, Logger logger)
    {
        try
        {
            _faculties.Add(faculty);
            logger.Debug("Worker added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            logger.Error("Worker not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }
    
    public List<Faculty> ReturnList(Logger logger)
    {
        logger.Debug("Return list" + Environment.NewLine);
        return _faculties;
    }
    
    internal List<Faculty> ReturnList()
    {
        return _faculties;
    }

    private static UniversityRepository _universityRepository;
    private static List<Administrator> _workerRep;
    private static List<Faculty> _faculties;
}