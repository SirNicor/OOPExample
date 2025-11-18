using Repository;

namespace UJob;

using Logger;
using UCore;
public class SalaryJob:IJob, ISalaryJob
{

    public SalaryJob(MyLogger myLogger, IWorkerTeacherRepository workerTeacherRepository, 
        IWorkerAdministratorRepository workerAdministratorRepository)
    {
        _myLogger = myLogger;
        _workerTeacherRepository = workerTeacherRepository;
        _workerAdministratorsRepository = workerAdministratorRepository;
    }


    public void DoWork()
    {
        List<Teacher> teachers = _workerTeacherRepository.ReturnList();
        List<Administrator> administrators = _workerAdministratorsRepository.ReturnListAdministrator();
        List<Worker> workers = new List<Worker>();
        foreach(var worker in teachers)
        {
            workers.Add(worker);
        }
        foreach(var worker in administrators)
        {
            workers.Add(worker);
        }
        int lenWorkers = workers.Count;
        int[] salaryWorkers = new int[lenWorkers];;
        for(int i = 0; i < lenWorkers; i++)
        {
            salaryWorkers[i] = workers[i].Salary;
        }
        
        int maxindex = 0, minindex = 0, FullSalary = 0;
        for (int i = 0; i < lenWorkers; i++)
        {
            
            if (salaryWorkers[i] > salaryWorkers[maxindex])
            {
                maxindex = i;
            }
            if (salaryWorkers[i] < salaryWorkers[minindex])
            {
                minindex = i;
            }
            FullSalary += salaryWorkers[i];
        }
        _myLogger.Info($"Максимальная зп = {salaryWorkers[maxindex]}. Рабочий с данной зп:");
        workers[maxindex].PrintInfo(_myLogger);
        _myLogger.Info($"Минимальная зп = {salaryWorkers[minindex]}. Рабочий с данной зп:");
        workers[minindex].PrintInfo(_myLogger);
        _myLogger.Info("Средняя зп = " + (double)FullSalary/lenWorkers);
    }
    private readonly MyLogger _myLogger;
    private Timer _timer;
    private IWorkerTeacherRepository _workerTeacherRepository;
    private IWorkerAdministratorRepository _workerAdministratorsRepository;
}   