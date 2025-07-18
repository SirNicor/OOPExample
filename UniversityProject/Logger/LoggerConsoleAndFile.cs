namespace Logger;

public class LoggerConsoleAndFile:Logger
{
    private readonly Logger[] _loggers;

    public LoggerConsoleAndFile(Logger[] loggers)
    {
        _loggers = loggers;
    }

    public override void Log(LevelLoger levelLoger, string message)
    {
        Log(levelLoger, message, null);
    }

    public override void Log(LevelLoger levelLoger, string message, Exception exception)
    {
        foreach (var loggers in _loggers)
        {
            loggers.Log(levelLoger, message);
        }
    }
}