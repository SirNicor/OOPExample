using Logger;
using Repository;
using UCore;
using UJob;
using Microsoft.Extensions.Logging.Configuration;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Configuration.AddJsonFile("appsettings.json");

app.Run(async (context) =>
{;
    MyLogger logger;
    IConfiguration appConfig = builder.Configuration;
    ConfigController configController = new ConfigController(appConfig);
    List<string> config = configController.Get().ToList();
    switch (config[1])
    {
        case "Console":
            logger = new ConsoleMyLogger();
            break;
        case "File":
            logger = new FileMyLogger();
            break;
        case "All":
            logger = new AllMyLogger( new MyLogger[] {new ConsoleMyLogger(),  new FileMyLogger()});
            break;
        default:
            logger = new FileMyLogger();
            break;
    }
    switch(config[0])
    {
        case "Information":
            logger.MinLog = LevelLoger.INFO;
            break;
        case "Warning":
            logger.MinLog = LevelLoger.WARNING;
            break;
        case "Error":
            logger.MinLog = LevelLoger.ERROR;
            break;
        case "Debug":
            logger.MinLog = LevelLoger.DEBUG;
            break;
        case "Fatal":
            logger.MinLog = LevelLoger.FATAL;
            break;
    }
    StudentRepository studentRepository = new StudentRepository(logger);
    WorkerRepository workerRepositoryAdministrator = new WorkerRepository(logger);
    UniversityRepository universityRepository = new UniversityRepository(logger, workerRepositoryAdministrator);
    FacultyRepository facultyRepository = new FacultyRepository(logger, universityRepository, workerRepositoryAdministrator);
    DepartmentRepository departmentRepository = new DepartmentRepository(logger, facultyRepository, workerRepositoryAdministrator);
    DirectionRepository directionRepository = new DirectionRepository(logger, studentRepository, departmentRepository);
    DisciplineRepository disciplineRepository = new DisciplineRepository(directionRepository);
    WorkerRepository workerRepositoryTeachers = new WorkerRepository(logger, disciplineRepository);
    SalaryJob salaryJob = new SalaryJob(logger, workerRepositoryTeachers, workerRepositoryAdministrator);
    PrintWorkersJob printWorkersJob = new PrintWorkersJob(logger, workerRepositoryTeachers, workerRepositoryAdministrator);
    PrintStudentsJob printStudentsJob = new PrintStudentsJob(logger, studentRepository);
    InfoCouplesAttendanceJob infoCouplesAttendanceJob = new InfoCouplesAttendanceJob(logger, studentRepository);
    ScoresOfStudentsJob scoresOfStudentsJob = new ScoresOfStudentsJob(logger, studentRepository);
    List<Teacher> teachers = workerRepositoryTeachers.ReturnListTeachers(logger);
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
                            logger.Info("Выход за возможный выбор");
                            Console.WriteLine("Повторите ввод");
                            break;
                    }
                }
            });
            
            threadOfJob.Start();
            threadOfWork.Start();
            threadOfSession.Start();
            threadOfInfo.Start();
});
app.Run();
