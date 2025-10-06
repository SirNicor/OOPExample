using Logger;
using Repository;
using UJob;
using Start;
using UCore;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

IConfiguration appConfig = builder.Configuration;
ConfigurationLogger cl = new ConfigurationLogger(appConfig);
MyLogger logger = cl.Get();
builder.Services.AddInfrastructureServices(logger, appConfig);
builder.Services.MakeCronJob(appConfig);
var app = builder.Build();

app.MapGet("/", async (HttpContext context) =>
{
    context.Response.Headers.ContentLanguage = "ru-RU";
    context.Response.Headers.ContentType = "text/html; charset=utf-8";
    context.Response.Headers.Append("University", "system");
    await context.Response.SendFileAsync("index.html");
}); 
app.Map("/PrintWorker", () => app.Services.GetService<IPrintWorkersJob>().DoWork());
app.Map("/PrintStudent", () => app.Services.GetService<IPrintStudentJob>().DoWork());
app.Map("/ScoresOfStudents", () => app.Services.GetService<IScoresOfStudentsJob>().DoWork());
app.Map("/InfoCouplesAttendance", () => app.Services.GetService<IInfoCouplesAttendanceJob>().DoWork());
app.Map("/GetJsonStudent/{id}", async (string id, HttpContext context) =>
{
    int i = int.Parse(id);
    var service = context.RequestServices.GetService<ReturnOneStudent>();   
    var student = service.ReturnStudent(i);
    student.PrintInfo(logger);
    await context.Response.WriteAsJsonAsync(student);
});
app.Map("/GetJsonStudents", async context =>
{
    var service = context.RequestServices.GetService<ReturnListOfStudents>();
    var students = service.ReturnList();
    await context.Response.WriteAsJsonAsync(students);
});
/*app.Map("/Create", async () =>
{
    Student student1 = new Student();
    student1.CountOfExamsPassed = 0;
    student1.CreditScores = 0;
    student1.SkipHours = 0;
    Console.WriteLine("Введите курс:");
    student1.Course = int.Parse(Console.ReadLine());
    Console.WriteLine("Введите информацию о наличие судимости:");
    student1.CriminalRecord = Convert.ToBoolean(Console.ReadLine());
    Console.WriteLine("Введите информацию о категории годности: ");
    student1.MilitaryIdAvailability = IdMillitary.DidNotServe;
    Console.Write
    student1.Passport = new Passport(2346, 111111, "St2", "St22", "St222", new DateTime(2007,11, 21),
        new Address("Russia", "Moscow", "St1", 2), "1");
    studentRepository.Create(student1, logger);
})*/
app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
    string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
app.Run();
