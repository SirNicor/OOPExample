namespace UJob;
using Repository;
using Logger;
using UCore;
public class PrintWorkersJob:IJob
{
    public PrintWorkersJob(MyLogger myLogger, WorkerRepository workerRepositoryTeachers, WorkerRepository workerRepositoryAdministrators)
    {
        _myLogger = myLogger;
        _workerRepositoryTeachers = workerRepositoryTeachers;
        _workerRepositoryAdministrators = workerRepositoryAdministrators;
    }

    public void DoWork()
    {
        _workerRepositoryTeachers.PrintAll(_myLogger);
        _workerRepositoryAdministrators.PrintAll(_myLogger);
    }
    
    
    private readonly MyLogger _myLogger;
    private Timer _timer;
    private WorkerRepository _repository;
    private WorkerRepository _workerRepositoryTeachers;
    private WorkerRepository _workerRepositoryAdministrators;
}