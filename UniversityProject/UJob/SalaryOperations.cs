namespace University.UJob;
using Logger;
using University.UCore;
public class SalaryOperations:IJob
{

    public SalaryOperations(Logger logger)
    {
        _logger = logger;
    }


    public void DoWork()
    {
        _logger.Debug("TestJob1");
        WorkerRepository repository = new WorkerRepository();
        repository.PrintAll(_logger);
        List<Worker> workers = repository.ReturnList(_logger);
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
}   