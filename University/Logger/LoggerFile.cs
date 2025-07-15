namespace University.Logger;

class LoggerFile : Logger
{   
    public LoggerFile(string fileName = @"D:\LessonsRep\OOPExample\OOPExample\University\Logs\Info.txt")
    {
        _fileName = fileName;
    }
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
        File.AppendAllText(_fileName, logMessage + Environment.NewLine);
    }
    private readonly string _fileName = @"D:\LessonsRep\OOPExample\OOPExample\University\Logs\Info.txt";
}