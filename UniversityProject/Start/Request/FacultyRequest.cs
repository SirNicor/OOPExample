using IRepositoryAll;
using Logger;
using UCore;

namespace Start.Request;

static public class FacultyRequest
{
    public static void AddFacultyRequest(this IEndpointRouteBuilder app, MyLogger logger)
    {
        app.MapGet("/Faculty/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IFacultyRepository>();   
            var faculty = service.Get(id);
            await context.Response.WriteAsJsonAsync(faculty); 
        });
        app.MapGet("/Faculty", async context =>
        {
            var service = context.RequestServices.GetService<IFacultyRepository>();
            var faculties = service.ReturnList();            
            await context.Response.WriteAsJsonAsync(faculties);
        });
        app.MapPut("/Faculty/{id}", async (long id, HttpContext context) =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IFacultyRepository>();
            FacultyDto faculty = await request.ReadFromJsonAsync<FacultyDto>();
            faculty.IdFaculty = id;
            var update = service.Update(faculty);
            await context.Response.WriteAsJsonAsync(update);
        });
        app.MapPost("/Faculty", async context =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IFacultyRepository>();
            FacultyDto faculty = await request.ReadFromJsonAsync<FacultyDto>();
            var id = service.Create(faculty);
            await context.Response.WriteAsJsonAsync(id);
        });
        app.MapDelete("/Faculty/{id}", (long id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IFacultyRepository>();
            service.Delete(id);
            return Task.CompletedTask;
        });
    }
}