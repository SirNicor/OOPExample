namespace UJob;
using Repository;
using Logger;
using UCore;

public class PrintStudentsJob:IJob
{
    public PrintStudentsJob(Logger logger, StudentRepository studentRepository)
    {
        _logger = logger;
        _studentsRepository = studentRepository;
    }

    public void DoWork()
    {
        _studentsRepository.PrintAll(_logger);
    }
    
    
    private readonly Logger _logger;
    private Timer _timer;
    private StudentRepository _studentsRepository;
}