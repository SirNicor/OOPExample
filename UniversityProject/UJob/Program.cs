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
            try
            {
                Logger.Logger logger = new ConsoleLogger();
                logger = new AllLogger([new ConsoleLogger(), new FileLogger()]);
                WorkerRepository workerRepository = new WorkerRepository();
                Passport passport = new Passport(5555, 555555, "Timofey", "Kryukov", 
                    new DateTime(1979,3,04), new Address("Russia", "Saint Petersburg", "Nalichnaya Street", 70), "1");;
                Worker worker = new Teacher(47000, passport, IdMillitary.InStock, false);
                workerRepository.Add(worker, logger);
                passport = new Passport(6767, 676767, "1", "2", 
                    new DateTime(1989,4,04), new Address("Russia", "Saint Petersburg", "Nalichnaya",  90), "1");;
                worker = new Teacher(87000, passport, IdMillitary.InStock, false);
                workerRepository.Add(worker, logger);
                while (true)
                {
                    SalaryOperations salaryOperations = new SalaryOperations(logger);
                    salaryOperations.DoWork();
                    Thread.Sleep(60000);
                }
            }
            catch
            {
                
            }
        }
    }
}   
