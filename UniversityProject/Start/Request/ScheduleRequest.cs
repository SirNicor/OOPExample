using Logger;
using Repository;
using UCore;
namespace Start;
static class ScheduleRequest
{
    public static void AddScheduleRequest(this IEndpointRouteBuilder app, MyLogger logger)
    {
        app.MapGet("/Schedule/{id}", async (int id, HttpContext context) =>
        {
            var request = context.Request;
            var service = context.RequestServices.GetService<IScheduleRepository>();
            var schedule = service.Get(id);
            await context.Response.WriteAsJsonAsync(schedule);
        });
        app.MapGet("/Schedule", async context =>
        {
            var request = context.Request;
            var service = context.RequestServices.GetService<IScheduleRepository>();
            var schedule = service.ReturnList();
            await context.Response.WriteAsJsonAsync(schedule);
        });
        app.MapPost("/Student", async context =>
        {
            var request = context.Request;
            var service = context.RequestServices.GetService<IStudentRepository>();
            Student student1 = await request.ReadFromJsonAsync<Student>();
            var ID = service.Create(student1);
            await context.Response.WriteAsJsonAsync(ID);
        });
        app.MapPut("/Student/{id}", async (long id, HttpContext context) =>
        {
            var request = context.Request;
            var service = context.RequestServices.GetService<IStudentRepository>();
            Student student = await request.ReadFromJsonAsync<Student>();
            student.PersonId = id;
            var Id = service.Update(student);
            await context.Response.WriteAsJsonAsync(Id);
        });
        app.MapDelete("/Student/{id}", async (long id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IStudentRepository>();
            service.Delete(id);
        });
    }
}