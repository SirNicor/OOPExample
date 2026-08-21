namespace Logger;

public class AllMyLogger(MyLogger[] loggers) : MyLogger
{
    protected override void Log(LevelLoger levelLoger, string message)
    {
        Log(levelLoger, message, null);
    }

    protected override void Log(LevelLoger levelLoger, string message, Exception exception)
    {
        foreach (var loggers1 in loggers)
        {
            switch (levelLoger)
            {
                case LevelLoger.DEBUG:
                    loggers1.Debug(message);
                    break;
                case LevelLoger.INFO:
                    loggers1.Info(message);
                    break;
                case LevelLoger.WARNING:
                    loggers1.Warning(message);
                    break;
                case LevelLoger.ERROR:
                    loggers1.Error(message);
                    break;
                case LevelLoger.FATAL:
                    loggers1.Fatal(message);
                    break;
            }
        }
    }
}   