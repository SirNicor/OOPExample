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
            Logger.Logger logger = new ConsoleLogger();
            logger = new FileLogger();
            SalaryOperations salaryOperations = new SalaryOperations(logger);
            salaryOperations.DoWork();
        }
    }
}   
