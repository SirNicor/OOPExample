using Logger;
using UJob;
using Start;
using Repository.Migrations;
using ApiTelegramBot;

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
    app.AddStudentRequest(app.Services.GetRequiredService<MyLogger>(), 
        app.Services.GetRequiredService<IConfiguration>());
    app.AddOtherRequest(app.Services.GetRequiredService<MyLogger>(), 
        app.Services.GetRequiredService<IConfiguration>());
    app.AddAddressRequest(app.Services.GetRequiredService<MyLogger>());
    app.AddPassportRequest(app.Services.GetRequiredService<MyLogger>());
    app.AddAdministratorRequest(app.Services.GetRequiredService<MyLogger>());
    app.AddUniversityRequest(app.Services.GetRequiredService<MyLogger>());
    app.AddFacultyRequest(app.Services.GetRequiredService<MyLogger>());
    app.AddDepartmentRequest(app.Services.GetRequiredService<MyLogger>());
    app.AddTeacherRequest(app.Services.GetRequiredService<MyLogger>());
    app.AddDisciplineRequest(app.Services.GetRequiredService<MyLogger>());
    app.AddDirectionRequest(app.Services.GetRequiredService<MyLogger>());
    using (var botScope = app.Services.CreateScope())
    {
        var bot = botScope.ServiceProvider.GetRequiredService<IStartBot>();
        _ = Task.Run(async () => await bot.ListenForMessagesAsync());
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
    