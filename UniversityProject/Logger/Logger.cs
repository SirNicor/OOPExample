using Microsoft.VisualBasic.CompilerServices;

namespace Logger;

public abstract class Logger
{
    protected LevelLoger LevelLoger = LevelLoger.DEBUG;
    protected DateTime CurrentTime;
    public abstract void Log(LevelLoger levelLoger,  string message);
    public abstract void Log(LevelLoger levelLoger, string message, Exception exception);
}