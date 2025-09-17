namespace UJob;
using Repository;
using Logger;
using UCore;
public class PrintWorkersJob:IJob, IPrintWorkersJob
{
    public PrintWorkersJob(MyLogger myLogger, IWorkerTeacherRepository workerTeacherRepository, 
        IWorkerAdministratorRepository workerAdministratorsRepository)
    {
        _myLogger = myLogger;
        _workerTeacherRepository = workerTeacherRepository;
        _workerAdministratorRepository = workerAdministratorsRepository;
    }

    public void DoWork()
    {
        _workerTeacherRepository.PrintAll(_myLogger);
        _workerAdministratorRepository.PrintAll(_myLogger);
    }
    
    
    private readonly MyLogger _myLogger;
    private Timer _timer;
    private IWorkerTeacherRepository _workerTeacherRepository;
    private IWorkerAdministratorRepository _workerAdministratorRepository;
}