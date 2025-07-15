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
            Direction AppliedComputerScience = new Direction(
                "FITM", new Administrator(), new Administrator(), 
                "IMST", new Administrator(), "AppliedComputerScience");
            Console.WriteLine(AppliedComputerScience.ReturnGroupCipher());
            
            Student Student0 = new Student(firstName: "John", lastName: "Walker", 18, 
                new Address("Russia", "Samara", "Streer1", 82), DegreesStudy.bachelor, 1, AppliedComputerScience);
            Student Student1 = new Student(firstName: "James", lastName: "Holland", "Matveevich",19, 
                new Address("Russia", "Samara", "Streer2", 44), DegreesStudy.bachelor, 2,  AppliedComputerScience);
            Student Student2 = new Student(firstName: "Alice", lastName: "Artemovna ", "Zaitseva", 20, 
                new Address("Russia", "Samara", "Streer1", 32), DegreesStudy.bachelor, 3,   AppliedComputerScience);
            Elder Student_elder0 = new Elder("Zakhar", "Bondarev", "Ivanovich", 20, 
                new Address("Russia", "Samara", "Streer2", 43), DegreesStudy.bachelor, 1,  AppliedComputerScience);

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
