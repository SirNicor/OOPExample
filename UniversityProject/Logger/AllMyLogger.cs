namespace Logger;

public class AllMyLogger:MyLogger
{
    private readonly MyLogger[] _loggers;

    public AllMyLogger(MyLogger[] loggers)
    {
        _loggers = loggers;
    }

    protected override void Log(LevelLoger levelLoger, string message)
    {
        Log(levelLoger, message, null);
    }

    protected override void Log(LevelLoger levelLoger, string message, Exception exception)
    {
        foreach (var loggers in _loggers)
        {
            switch (levelLoger)
            {
                case LevelLoger.DEBUG:
                    loggers.Debug(message);
                    break;
                case LevelLoger.INFO:
                    loggers.Info(message);
                    break;
                case LevelLoger.WARNING:
                    loggers.Warning(message);
                    break;
                case LevelLoger.ERROR:
                    loggers.Error(message);
                    break;
                case LevelLoger.FATAL:
                    loggers.Fatal(message);
                    break;
            }
        }
    }
}   