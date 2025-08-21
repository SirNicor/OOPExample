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
            Logger logger = new ConsoleLogger();
            
            StudentRepository studentRepository = new StudentRepository(logger);
            WorkerRepository workerRepositoryAdministrator = new WorkerRepository(logger);
            UniversityRepository universityRepository = new UniversityRepository(logger, workerRepositoryAdministrator);
            FacultyRepository facultyRepository = new FacultyRepository(logger, universityRepository, workerRepositoryAdministrator);
            DepartmentRepository departmentRepository = new DepartmentRepository(logger, facultyRepository, workerRepositoryAdministrator);
            DirectionRepository directionRepository = new DirectionRepository(logger, studentRepository, departmentRepository);
            DisciplineRepository disciplineRepository = new DisciplineRepository(directionRepository);
            WorkerRepository workerRepositoryTeachers = new WorkerRepository(logger, disciplineRepository);
            
            SalaryOperationsJob salaryOperationsJob = new SalaryOperationsJob(logger, workerRepositoryTeachers, workerRepositoryAdministrator);
            
            List<Teacher> teachers = workerRepositoryTeachers.ReturnListTeachers(logger);
            
            Thread threadOfJob = new Thread(() =>
            {
                while (true)
                {
                    salaryOperationsJob.DoWork();
                    Thread.Sleep(60000);
                }
            });

            Thread threadOfWork = new Thread(() =>
            {
                while (true)
                {
                    foreach (var teacher in teachers)
                    {
                        teacher.DoWork(logger);
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
                        teacher.DoSession(logger);
                        Thread.Sleep(240000);
                    }
                }
            });
            threadOfWork.Priority = ThreadPriority.AboveNormal;
            
            threadOfJob.Start();
            threadOfWork.Start();
            threadOfSession.Start();
        }
    }