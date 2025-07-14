namespace University.Logger;

class LoggerFile : ILogger
{   
    public void Log(ILogger logger, LevelLoger levelLoger, string message)
    {
        Log(logger, levelLoger, message, null);
    }
    public void Log(ILogger logger, LevelLoger levelLoger, string message, Exception exception)
    {
        throw new NotImplementedException();
    }
    private static string FileName = "logFilename.log";
}