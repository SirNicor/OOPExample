using Microsoft.VisualBasic.CompilerServices;

namespace University.Logger;

interface ILogger
{
    protected static LevelLoger LevelLoger = LevelLoger.DEBUG;
    void Log(ILogger logger, LevelLoger levelLoger,  string message);
    void Log(ILogger logger, LevelLoger levelLoger, string message, Exception exception);
}