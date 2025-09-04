namespace UJob;

using System;
using System.Runtime.InteropServices;
using Logger;
using Repository;
using UCore;
    class Program
    {
        static void Main()
        {
            
            
            MyLogger myLogger = new ConsoleMyLogger();
                
            StudentRepository studentRepository = new StudentRepository(myLogger);
            WorkerRepository workerRepositoryAdministrator = new WorkerRepository(myLogger);
            UniversityRepository universityRepository = new UniversityRepository(myLogger, workerRepositoryAdministrator);
            FacultyRepository facultyRepository = new FacultyRepository(myLogger, universityRepository, workerRepositoryAdministrator);
            DepartmentRepository departmentRepository = new DepartmentRepository(myLogger, facultyRepository, workerRepositoryAdministrator);
            DirectionRepository directionRepository = new DirectionRepository(myLogger, studentRepository, departmentRepository);
            DisciplineRepository disciplineRepository = new DisciplineRepository(directionRepository);
            WorkerRepository workerRepositoryTeachers = new WorkerRepository(myLogger, disciplineRepository);
            
            SalaryJob salaryJob = new SalaryJob(myLogger, workerRepositoryTeachers, workerRepositoryAdministrator);
            PrintWorkersJob printWorkersJob = new PrintWorkersJob(myLogger, workerRepositoryTeachers, workerRepositoryAdministrator);
            PrintStudentsJob printStudentsJob = new PrintStudentsJob(myLogger, studentRepository);
            InfoCouplesAttendanceJob infoCouplesAttendanceJob = new InfoCouplesAttendanceJob(myLogger, studentRepository);
            ScoresOfStudentsJob scoresOfStudentsJob = new ScoresOfStudentsJob(myLogger, studentRepository);
            
            
            List<Teacher> teachers = workerRepositoryTeachers.ReturnListTeachers(myLogger);
            
            Thread threadOfJob = new Thread(() =>
            {
                while (true)
                {
                    salaryJob.DoWork();
                    Thread.Sleep(60000);
                }
            });

            Thread threadOfWork = new Thread(() =>
            {
                while (true)
                {
                    foreach (var teacher in teachers)
                    {
                        teacher.DoWork(myLogger);
                        Thread.Sleep(120000);
                    }
                }
            });
            
            threadOfWork.Priority = ThreadPriority.AboveNormal;
            
            Thread threadOfSession = new Thread(() =>
            {
                while (true)
                {
                    foreach (var teacher in teachers)
                    {
                        teacher.DoSession(myLogger);
                        Thread.Sleep(240000);
                    }
                }
            });
            threadOfWork.Priority = ThreadPriority.AboveNormal;

            Thread threadOfInfo = new Thread(() =>
            {
                int input;
                while (true)
                {
                    Console.WriteLine("Вывод интересующей вас инфо. Если о рабочих, введите 1, если о студентах, введите 2. Если о баллах студентов - 3, если о пропусках студентов - 4");
                    input = int.Parse(Console.ReadLine());
                    switch (input)
                    {
                        case 1:
                            printWorkersJob.DoWork();
                            break;
                        case 2:
                            printStudentsJob.DoWork();
                            break;
                        case 3:
                            scoresOfStudentsJob.DoWork();
                            break;
                        case 4:
                            infoCouplesAttendanceJob.DoWork();
                            break;
                        default:
                            myLogger.Info("Выход за возможный выбор");
                            Console.WriteLine("Повторите ввод");
                            break;
                    }
                }
            });
            
            threadOfJob.Start();
            threadOfWork.Start();
            threadOfSession.Start();
            threadOfInfo.Start();
        }
    }