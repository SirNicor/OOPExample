using System.ComponentModel.DataAnnotations;
using Logger;
using Repository;
using UJob;
using Start;
using UCore;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Repository.Migrations;
using ApiTelegramBot;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
IConfiguration appConfig = builder.Configuration;
ConfigurationLogger cl = new ConfigurationLogger(appConfig);
MyLogger logger = cl.Get();
try
{
    StartMigrations startMigrations = new StartMigrations(appConfig, logger);
    startMigrations.Start();
    builder.Services.AddInfrastructureServices(logger, appConfig);
    builder.Services.MakeCronJob(appConfig);
    var app = builder.Build();
    using (var scope = app.Services.CreateScope())
    {
        
        app.AddStudentRequest(scope.ServiceProvider.GetRequiredService<MyLogger>(), 
            scope.ServiceProvider.GetRequiredService<IConfiguration>());
        app.AddOtherRequest(scope.ServiceProvider.GetRequiredService<MyLogger>(), 
            scope.ServiceProvider.GetRequiredService<IConfiguration>());
        app.AddAddressRequest(scope.ServiceProvider.GetRequiredService<MyLogger>());
        app.AddPassportRequest(scope.ServiceProvider.GetRequiredService<MyLogger>());
        app.AddAdministratorRequest(scope.ServiceProvider.GetRequiredService<MyLogger>());
        app.AddUniversityRequest(scope.ServiceProvider.GetRequiredService<MyLogger>());
        app.AddFacultyRequest(scope.ServiceProvider.GetRequiredService<MyLogger>());
        app.AddDepartmentRequest(scope.ServiceProvider.GetRequiredService<MyLogger>());
        app.AddTeacherRequest(scope.ServiceProvider.GetRequiredService<MyLogger>());
        app.AddDisciplineRequest(scope.ServiceProvider.GetRequiredService<MyLogger>());
        app.AddDirectionRequest(scope.ServiceProvider.GetRequiredService<MyLogger>());
        var bot = scope.ServiceProvider.GetRequiredService<IStartBot>();
        await bot.ListenForMessagesAsync();
    }
    app.MapGet("/", async (HttpContext context) =>
    {
        context.Response.Headers.ContentLanguage = "ru-RU";
        context.Response.Headers.ContentType = "text/html; charset=utf-8";
        context.Response.Headers.Append("University", "system");
        await context.Response.SendFileAsync("index.html");
    }); 
    app.MapGet("/Worker", () => app.Services.GetService<IPrintWorkersJob>().DoWork());
    app.MapGet("/ScoresOfStudents", () => app.Services.GetService<IScoresOfStudentsJob>().DoWork());
    app.MapGet("/InfoCouplesAttendance", () => app.Services.GetService<IInfoCouplesAttendanceJob>().DoWork());
    app.Run();
}
catch (Exception ex)
{
    logger.Error("Error in general trycatch " + ex.Message + ex.Source + ex.StackTrace + ex.TargetSite);
}
    