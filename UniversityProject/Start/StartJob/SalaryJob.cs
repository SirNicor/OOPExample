using ServiceWorkerCronJobDemo.Services;
using Logger;
using Repository;
using UCore;
using UJob;
namespace Start;

public class SalaryJob: CronJobService
{

    public SalaryJob(IScheduleConfig<TeacherDoWorkJob> config, ILogger<TeacherDoWorkJob> loggerMain,
        MyLogger myLogger, ISalaryJob salaryJob)
        : base(config.CronExpression, config.TimeZoneInfo, loggerMain)
    {
        _loggerMain = loggerMain;
        _myLogger = myLogger;
        _salaryJob = salaryJob;
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
        _salaryJob.DoWork();
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
    private ISalaryJob _salaryJob;
}