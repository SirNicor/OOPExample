namespace UJob;
using Repository;
using Logger;
using UCore;
public class PrintWorkersJob:IJob
{
    public PrintWorkersJob(Logger logger, WorkerRepository workerRepositoryTeachers, WorkerRepository workerRepositoryAdministrators)
    {
        _logger = logger;
        _workerRepositoryTeachers = workerRepositoryTeachers;
        _workerRepositoryAdministrators = workerRepositoryAdministrators;
    }

    public void DoWork()
    {
        _workerRepositoryTeachers.PrintAll(_logger);
        _workerRepositoryAdministrators.PrintAll(_logger);
    }
    
    
    private readonly Logger _logger;
    private Timer _timer;
    private WorkerRepository _repository;
    private WorkerRepository _workerRepositoryTeachers;
    private WorkerRepository _workerRepositoryAdministrators;
}