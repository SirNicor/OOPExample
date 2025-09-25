using ServiceWorkerCronJobDemo.Services;
using Logger;
using Repository;
using UCore;
using UJob;
namespace Start;

public class InfoJob: CronJobService
{

    public InfoJob(IScheduleConfig<TeacherDoWorkJob> config, ILogger<TeacherDoWorkJob> loggerMain,
        MyLogger myLogger, IPrintWorkersJob printWorkersJob, IPrintStudentJob printStudentJob, 
        IScoresOfStudentsJob scoresOfStudentsJob, IInfoCouplesAttendanceJob infoCouplesAttendanceJob)
        : base(config.CronExpression, config.TimeZoneInfo, loggerMain)
    {
        _loggerMain = loggerMain;
        _myLogger = myLogger;
        _printWorkersJob = printWorkersJob;
        _printStudentJob = printStudentJob;
        _scoresOfStudentsJob = scoresOfStudentsJob;
        _infoCouplesAttendanceJob = infoCouplesAttendanceJob;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _loggerMain.LogInformation("CronJob запущен");
        _myLogger.Info("CronJob запущен");
        return base.StartAsync(cancellationToken);
    }

    public override Task DoWork(CancellationToken cancellationToken)
    {
        _loggerMain.LogInformation($"{DateTime.Now:hh:mm:ss} Выполняется задача");
        _myLogger.Info($"{DateTime.Now:hh:mm:ss} Выполняется задача"); 
        Console.WriteLine("Вывод интересующей вас инфо. Если о рабочих, введите 1, если о студентах, введите 2. Если о баллах студентов - 3, если о пропусках студентов - 4");
        int input = int.Parse(Console.ReadLine()??"0");
        switch (input)
        {
            case 1:
                _printWorkersJob.DoWork();
                break;
            case 2:
                _printStudentJob.DoWork();
                break;
            case 3:
                _scoresOfStudentsJob.DoWork();
                break;
            case 4:
                _infoCouplesAttendanceJob.DoWork();
                break;
            default:
                _myLogger.Info("Выход за возможный выбор");
                Console.WriteLine("Повторите ввод");
                break;
        }
        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _loggerMain.LogInformation("CronJob остановлен");
        _myLogger.Info("CronJob остановлен");
        return base.StopAsync(cancellationToken);
    }
    
    private readonly ILogger<TeacherDoWorkJob> _loggerMain;
    private readonly MyLogger _myLogger;
    private IInfoCouplesAttendanceJob _infoCouplesAttendanceJob;
    private IScoresOfStudentsJob _scoresOfStudentsJob;
    private IPrintStudentJob  _printStudentJob;
    private IPrintWorkersJob _printWorkersJob;
}