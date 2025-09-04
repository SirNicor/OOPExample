namespace Logger;

public class ConsoleMyLogger : MyLogger
{
    protected override void Log(LevelLoger levelLoger, string message)
    {
        Log(levelLoger, message, null);
    }
    protected override void Log(LevelLoger levelLoger, string message, Exception exception)
    {
        if (levelLoger < MinLog)
            return;
        CurrentTime = DateTime.Now;
        var logMessage = $"{CurrentTime}: {levelLoger}: {message}";
        if (exception != null)
        {
            logMessage += Environment.NewLine + exception.StackTrace;
        }
        Console.WriteLine(logMessage);
    }
}