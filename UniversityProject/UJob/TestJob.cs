namespace University.UJob;
using Logger;
using University.UCore;
public class TestJob:IJob
{
    private readonly Logger _logger;

    public TestJob(Logger logger)
    {
        _logger = logger;
    }


    public void DoWork()
    {
        _logger.Log(LevelLoger.DEBUG, "TestJob1");
        WorkerRepository repository = new WorkerRepository();
    }
}