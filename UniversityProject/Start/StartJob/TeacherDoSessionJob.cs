using ServiceWorkerCronJobDemo.Services;
using Logger;
using Repository;
using UCore;
namespace Start;

public class TeacherDoSessionJob : CronJobService
{
    public TeacherDoSessionJob(IScheduleConfig<TeacherDoWorkJob> config, ILogger<TeacherDoWorkJob> loggerMain, MyLogger myLogger, 
        IWorkerTeacherRepository teachers)
        : base(config.CronExpression, config.TimeZoneInfo, loggerMain)
    {
        _loggerMain = loggerMain;
        _myLogger = myLogger;
        _teachers = teachers.ReturnListTeachers(_myLogger);
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
        foreach (var teacher in _teachers)
        {
            teacher.DoSession(_myLogger);
            Thread.Sleep(120000);
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
    private List<Teacher> _teachers;
}