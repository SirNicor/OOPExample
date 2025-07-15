namespace University.Logger;

class LoggerConsole : Logger
{
    public override void Log(LevelLoger levelLoger, string message)
    {
        Log(levelLoger, message, null);
    }
    public override void Log(LevelLoger levelLoger, string message, Exception exception)
    {
        if (levelLoger < LevelLoger)
            return;
        CurrentTime = DateTime.Now;
        var logMessage = $"{CurrentTime}: {levelLoger}: {message}";
        if (exception != null)
        {
            logMessage += Environment.NewLine + exception;
        }
        Console.WriteLine(logMessage);
    }
}