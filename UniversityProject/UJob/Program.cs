using System;
using System.Runtime.InteropServices;
using Logger;
using University.UCore;

namespace University.UJob
{
    class Program
    {
        static void Main()
        {
            Logger.Logger logger = new LoggerConsole();
            logger = new LoggerFile();
            logger.Log(LevelLoger.INFO, "Started");
            IJob test0 = new TestJob(logger);
            logger = new LoggerConsoleAndFile(new Logger.Logger[] {new LoggerFile(), new LoggerFile()}); 
            IJob test1 = new TestJob2(logger);
            test0.DoWork();
            test1.DoWork();
        }
    }
}   
