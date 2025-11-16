using Logger;
namespace Start;
using Repository;
using UCore;

static class AdministratorRequest
{
    public static void AddAdministratorRequest(this IEndpointRouteBuilder app, MyLogger logger)
    {
        app.MapGet("/Administrator/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<ReturnOneAdministrator>();   
            var administrator = service.ReturnAdministrator(id);
            administrator.PrintInfo(logger);
            await context.Response.WriteAsJsonAsync(administrator); 
        });
        app.MapGet("/Administrator", async context =>
        {
            var service = context.RequestServices.GetService<ReturnListOfStudents>();
            var administrators = service.ReturnList();            
            await context.Response.WriteAsJsonAsync(administrators);
        });
        app.MapPost("/Administrator", async context =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IWorkerAdministratorRepository>();
            Administrator administrator = await request.ReadFromJsonAsync<Administrator>();
            var id = service.Create(administrator);
            await context.Response.WriteAsJsonAsync(id);
        });
        app.MapPut("/Administrator/{id}", async (long id, HttpContext context) =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IWorkerAdministratorRepository>();
            Administrator administrator = await request.ReadFromJsonAsync<Administrator>();
            administrator.PersonId = id;
            id = service.Update(administrator);
            await context.Response.WriteAsJsonAsync(id);
        });
        app.MapDelete("/Administrator/{id}", async (long id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IWorkerAdministratorRepository>();
            service.Delete(id);
        });
    }
}