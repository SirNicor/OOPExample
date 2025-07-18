namespace University.UJob;
using Logger;
using University.UCore;

public class TestJob2:IJob
{
    private readonly Logger _logger;

    public TestJob2(Logger logger)
    {
        _logger = logger;
    }

    public void DoWork()
    {
        _logger.Log(LevelLoger.DEBUG, "TestJob2");
    }
}