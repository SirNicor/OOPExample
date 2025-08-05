namespace Repository;
using UCore;
using Logger;

public class DepartmentRepository
{
    public DepartmentRepository(Logger logger, FacultyRepository facultyRepository, WorkerRepository workerRepository)
    {
        _facultyRepository = facultyRepository;
        WorkerRepository workerRep = workerRepository;
        foreach (var worker in workerRep.ReturnListAdministrator(logger))
        {
            if (worker.GetType() == typeof(Administrator))
            {
                _workerRep.Add((Administrator)worker);
            }
        } 
        _departments.Add(new Department("IIST", _workerRep[5],  _workerRep.GetRange(6, 1), _facultyRepository.ReturnList()[0]));
    }
    
    public void Add(Department department, Logger logger)
    {
        try
        {
            _departments.Add(department);
            logger.Debug("Worker added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            logger.Error("Worker not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }
    
    public List<Department> ReturnList(Logger logger)
    {
        logger.Debug("Return list" + Environment.NewLine);
        return _departments;
    }
    
    internal List<Department> ReturnList()
    {
        return _departments;
    }

    private static FacultyRepository _facultyRepository;
    private static List<Administrator> _workerRep = new List<Administrator>();
    private static List<Department> _departments = new List<Department>();
}