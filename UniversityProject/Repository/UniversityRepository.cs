namespace Repository;
using UCore;
using Logger;
public class UniversityRepository : IUniversityRepository
{
    public UniversityRepository(MyLogger myLogger, IWorkerAdministratorRepository workerAdministratorRepository)
    {
        IWorkerAdministratorRepository workerAdministratorRep = workerAdministratorRepository;
        foreach(var worker in workerAdministratorRep.ReturnListAdministrator(myLogger))
        {
            if (worker.GetType() == typeof(Administrator))
            {
                _workerRep.Add((Administrator)worker);
            }
        }
        _university.Add(new ClassUniversity("VUZ", _workerRep[0],  _workerRep.GetRange(1,1), new Random().Next(10000, 10000000)));
    }
    
    public void Add(ClassUniversity university, MyLogger myLogger)
    {
        try
        {
            _university.Add(university);
            myLogger.Debug("Worker added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            myLogger.Error("Worker not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }
    
    public List<ClassUniversity> ReturnList(MyLogger myLogger)
    {
        myLogger.Debug("Return list" + Environment.NewLine);
        return _university;
    }
    
    private static List<Administrator> _workerRep = new List<Administrator>();
    private static List<ClassUniversity> _university = new List<ClassUniversity>();
}