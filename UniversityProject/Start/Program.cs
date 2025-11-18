using System.ComponentModel.DataAnnotations;
using Logger;
using Repository;
using UJob;
using Start;
using UCore;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Repository.Migrations;

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
    app.AddStudentRequest(app.Services.GetService<MyLogger>(), app.Services.GetService<IConfiguration>());
    app.AddOtherRequest(app.Services.GetService<MyLogger>(), app.Services.GetService<IConfiguration>());
    app.AddAddressRequest(app.Services.GetService<MyLogger>());
    app.AddPassportRequest(app.Services.GetService<MyLogger>());
    app.AddAdministratorRequest(app.Services.GetService<MyLogger>());
    app.AddUniversityRequest(app.Services.GetService<MyLogger>());
    app.AddFacultyRequest(app.Services.GetService<MyLogger>());
    app.AddDepartmentRequest(app.Services.GetService<MyLogger>());
    app.AddTeacherRequest(app.Services.GetService<MyLogger>());
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
    