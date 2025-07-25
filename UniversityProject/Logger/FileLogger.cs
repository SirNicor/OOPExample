﻿namespace Logger;

public class FileLogger : Logger
{
    public FileLogger()
    {
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string workingDir = Environment.CurrentDirectory;
        _fileName = Path.Combine(workingDir, "Logs");
        if (!Directory.Exists(_fileName))
        {
            Directory.CreateDirectory(_fileName);
        }
        _fileName = Path.Combine(_fileName, $"log_{currentTime}.txt");
        File.AppendAllText(_fileName, DateTime.Now + ": создан новый лог файл" + Environment.NewLine);
    }
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
            logMessage += Environment.NewLine + exception;
        }
        File.AppendAllText(_fileName, logMessage + Environment.NewLine);
    }
    private readonly string _fileName;
}