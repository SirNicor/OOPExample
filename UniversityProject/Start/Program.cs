using Logger;
using Repository;
using UCore;
using UJob;
using Start;
using ServiceWorkerCronJobDemo;
using ServiceWorkerCronJobDemo.Services;
using SalaryJob = Start.SalaryJob;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

IConfiguration appConfig = builder.Configuration;
ConfigurationLogger cl = new ConfigurationLogger(appConfig);
MyLogger logger = cl.Get();
builder.Services.AddInfrastructureServices(logger);
builder.Services.MakeCronJob(appConfig);
var app = builder.Build();


app.Run(async (context) =>
{
    var response = context.Response;
    response.Headers.ContentLanguage = "ru-RU";
    response.Headers.ContentType = "text/html; charset=utf-8";
    response.Headers.Append("University", "system");
    response.SendFileAsync("Index.html");
    
    List<Teacher> teachers = app.Services.GetService<IWorkerTeacherRepository>().ReturnListTeachers(app.Services.GetService<MyLogger>());
    
    Thread threadOfInfo = new Thread(() =>
            {
                int input;
                while (true)
                {
                    Console.WriteLine("Вывод интересующей вас инфо. Если о рабочих, введите 1, если о студентах, введите 2. Если о баллах студентов - 3, если о пропусках студентов - 4");
                    input = int.Parse(Console.ReadLine()??"0");
                    switch (input)
                    {
                        case 1:
                            app.Services.GetService<IPrintWorkersJob>().DoWork();
                            break;
                        case 2:
                            app.Services.GetService<IPrintStudentJob>().DoWork();
                            break;
                        case 3:
                            app.Services.GetService<IScoresOfStudentsJob>().DoWork();
                            break;
                        case 4:
                            app.Services.GetService<IInfoCouplesAttendanceJob>().DoWork();
                            break;
                        default:
                            logger.Info("Выход за возможный выбор");
                            Console.WriteLine("Повторите ввод");
                            break;
                    }
                }
            });
    
            threadOfInfo.Start();
});
app.Run();
