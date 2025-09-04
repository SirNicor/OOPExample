using Logger;
using Repository;
using UJob;
using UCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Configuration.AddJsonFile("appsettings.json");
app.UseWelcomePage(); 
app.Run();


