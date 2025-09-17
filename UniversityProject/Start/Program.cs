using Logger;
using Repository;
using UCore;
using UJob;
using Microsoft.Extensions.Logging.Configuration;
using Newtonsoft.Json;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
IConfiguration appConfig1 = builder.Configuration;
ConfigurationLogger cl1 = new ConfigurationLogger(appConfig1);
MyLogger logger1 = cl1.Get();
builder.Services.AddSingleton<MyLogger>(logger1);
builder.Services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
builder.Services.AddSingleton<IDirectionRepository, DirectionRepository>();
builder.Services.AddSingleton<IDisciplineRepository,  DisciplineRepository>();
builder.Services.AddSingleton<IFacultyRepository, FacultyRepository>();
builder.Services.AddSingleton<IUniversityRepository, UniversityRepository>();
builder.Services.AddSingleton<IWorkerTeacherRepository, WorkerTeacherRepository>();
builder.Services.AddSingleton<IWorkerAdministratorRepository, WorkerAdministratorRepository>();
builder.Services.AddSingleton<IInfoCouplesAttendanceJob, InfoCouplesAttendanceJob>();
builder.Services.AddSingleton<IPrintStudentJob, PrintStudentsJob>();
builder.Services.AddSingleton<IPrintWorkersJob, PrintWorkersJob>();
builder.Services.AddSingleton<ISalaryJob, SalaryJob>();
builder.Services.AddSingleton<IScoresOfStudentsJob, ScoresOfStudentsJob>();
var app = builder.Build();


app.Run(async (context) =>
{
    var response = context.Response;
    response.Headers.ContentLanguage = "ru-RU";
    response.Headers.ContentType = "text/html; charset=utf-8";
    response.Headers.Append("University", "system");
    response.SendFileAsync("Index.html");
    
    Action<string>? PrintForClients = (string s1) => Console.WriteLine(s1);
    IConfiguration appConfig = builder.Configuration;
    ConfigurationLogger cl = new ConfigurationLogger(appConfig);
    MyLogger logger = cl.Get();
    
    Console.WriteLine(logger.MinLog);   
    List<Teacher> teachers = app.Services.GetService<IWorkerTeacherRepository>().ReturnListTeachers(app.Services.GetService<MyLogger>());
    Thread threadOfJob = new Thread(() =>
            {
                while (true)
                {
                    app.Services.GetService<ISalaryJob>();
                    Thread.Sleep(60000);
                }
            });

    Thread threadOfWork = new Thread(() =>
            {
                teachers = app.Services.GetService<IWorkerTeacherRepository>().ReturnListTeachers(app.Services.GetService<MyLogger>());
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
                    PrintForClients("Вывод интересующей вас инфо. Если о рабочих, введите 1, если о студентах, введите 2. Если о баллах студентов - 3, если о пропусках студентов - 4");
                    input = int.Parse(Console.ReadLine()??"0");
                    switch (input)
                    {
                        case 1:
                            app.Services.GetService<IPrintWorkersJob>();
                            break;
                        case 2:
                            app.Services.GetService<IPrintStudentJob>();
                            break;
                        case 3:
                            app.Services.GetService<IScoresOfStudentsJob>();
                            break;
                        case 4:
                            app.Services.GetService<IInfoCouplesAttendanceJob>();
                            break;
                        default:
                            logger.Info("Выход за возможный выбор");
                            PrintForClients("Повторите ввод");
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
