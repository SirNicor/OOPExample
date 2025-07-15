using System;
using System.Runtime.InteropServices;
using UCore;
using University.Logger;

namespace UJob
{
    class Program
    {
        static void Main()
        {
            LoggerConsole loggerConsole = new LoggerConsole();
            LoggerFile loggerFile = new LoggerFile();
            Logger[] Loggers = { loggerFile, loggerConsole };
            
            loggerConsole.Log(LevelLoger.DEBUG, "loggerConsole");
            loggerFile.Log(LevelLoger.DEBUG, "loggerFile");
            foreach (var Logger in Loggers)
            {
                Logger.Log(LevelLoger.INFO, "Logger");
            }
        }
    }
}   
