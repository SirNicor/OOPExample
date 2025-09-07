namespace Logger;

using System.Diagnostics;
using Microsoft.VisualBasic.CompilerServices;
using System.Text.Json;
public abstract class MyLogger
{
    public LevelLoger MinLog { get; set; }
    protected DateTime CurrentTime;
    protected abstract void Log(LevelLoger levelLoger,  string message);
    protected abstract void Log(LevelLoger levelLoger, string message, Exception exception);

    public void Debug(string message)
    {
        Log(LevelLoger.DEBUG, message);
    }

    public void Debug(string message, Exception exception)
    {
        Log(LevelLoger.DEBUG, message, exception);
    }

    public void Info(string message)
    {
        Log(LevelLoger.INFO, message);
    }

    public void Info(string message, Exception exception)
    {
        Log(LevelLoger.INFO, message, exception);
    }

    public void Warning(string message)
    {
        Log(LevelLoger.WARNING, message);
    }

    public void Warning(string message, Exception exception)
    {
        Log(LevelLoger.WARNING, message, exception);
    }

    public void Error(string message)
    {
        Log(LevelLoger.ERROR, message);
    }

    public void Error(string message, Exception exception)
    {
        Log(LevelLoger.ERROR, message, exception);
    }

    public void Fatal(string message)
    {
        Log(LevelLoger.FATAL, message);
    }

    public void Fatal(string message, Exception exception)
    {
        Log(LevelLoger.FATAL, message, exception);
    }
}