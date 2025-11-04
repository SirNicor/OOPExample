namespace Repository;
using UCore;
using Logger;

public class FacultyRepository : IFacultyRepository
{

    public FacultyRepository(MyLogger myLogger, IUniversityRepository universityRepository, IWorkerAdministratorRepository workerAdministratorRepository)
    {
        _universityRepository = universityRepository;
        IWorkerAdministratorRepository workerAdministratorRep = workerAdministratorRepository;
        foreach (var worker in workerAdministratorRep.ReturnListAdministrator())
        {
            if (worker.GetType() == typeof(Administrator))
            {
                _workerRep.Add((Administrator)worker);
            }
        } 
        _faculties.Add(new Faculty("FITU", _workerRep[2],  _workerRep[3], _workerRep.GetRange(4, 1), _universityRepository.ReturnList(myLogger)[0]));
    }
    
    public void Add(Faculty faculty, MyLogger myLogger)
    {
        try
        {
            _faculties.Add(faculty);
            myLogger.Debug("Faculty added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            myLogger.Error("Faculty not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }
    
    public List<Faculty> ReturnList(MyLogger myLogger)
    {
        myLogger.Debug("Return list" + Environment.NewLine);
        return _faculties;
    }

    private static IUniversityRepository _universityRepository;
    private static List<Administrator> _workerRep = new List<Administrator>();
    private static List<Faculty> _faculties = new List<Faculty>();
}