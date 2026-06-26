namespace UJob;
using Repository;
using Logger;
using UCore;
using IRepositoryAll;

public class PrintStudentsJob:IJob, IPrintStudentJob
{
    public PrintStudentsJob(MyLogger myLogger, IStudentRepository studentRepository)
    {
        _myLogger = myLogger;
        _studentsRepository = studentRepository;
    }

    public async Task DoWorkAsync()
    {
        await _studentsRepository.PrintAllAsync();
    }
    
    
    private readonly MyLogger _myLogger;
    private Timer _timer;
    private IStudentRepository _studentsRepository;
}