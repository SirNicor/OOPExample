namespace UJob;
using Repository;
using Logger;
using UCore;

public class PrintStudentsJob:IJob, IPrintStudentJob
{
    public PrintStudentsJob(MyLogger myLogger, IStudentRepository studentRepository)
    {
        _myLogger = myLogger;
        _studentsRepository = studentRepository;
    }

    public void DoWork()
    {
        _studentsRepository.PrintAll(_myLogger);
    }
    
    
    private readonly MyLogger _myLogger;
    private Timer _timer;
    private IStudentRepository _studentsRepository;
}