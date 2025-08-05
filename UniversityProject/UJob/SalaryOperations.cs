using Repository;

namespace UJob;

using Logger;
using UCore;
public class SalaryOperations:IJob
{

    public SalaryOperations(Logger logger, WorkerRepository workerRepositoryTeachers, WorkerRepository workerRepositoryAdministrators)
    {
        _logger = logger;
        _workerRepositoryTeachers = workerRepositoryTeachers;
        _workerRepositoryAdministrators = workerRepositoryAdministrators;
    }


    public void DoWork()
    {
        // _workerRepositoryTeachers.PrintAll(_logger);
        // _workerRepositoryAdministrators.PrintAll(_logger);
        List<Teacher> teachers = _workerRepositoryTeachers.ReturnListTeachers(_logger);
        List<Administrator> administrators = _workerRepositoryAdministrators.ReturnListAdministrator(_logger);
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
            salaryWorkers[i] = workers[i].ReturnSalary;
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
        _logger.Info($"Максимальная зп = {salaryWorkers[maxindex]}. Рабочий с данной зп:");
        workers[maxindex].PrintInfo(_logger);
        _logger.Info($"Минимальная зп = {salaryWorkers[minindex]}. Рабочий с данной зп:");
        workers[minindex].PrintInfo(_logger);
        _logger.Info("Средняя зп = " + (double)FullSalary/lenWorkers);
    }
    private readonly Logger _logger;
    private Timer _timer;
    private WorkerRepository _repository;
    private WorkerRepository _workerRepositoryTeachers;
    private WorkerRepository _workerRepositoryAdministrators;
}   