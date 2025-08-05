namespace UJob;
using Logger;
using UCore;

public class TestJob2:IJob
{
    private readonly Logger _logger;

    public TestJob2(Logger logger)
    {
        _logger = logger;
    }

    public void DoWork()
    {
        _logger.Debug("TestJob2");
    }
}