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
        app.MapPost("/Schedule", async context =>
        {
            var request = context.Request;
            var service = context.RequestServices.GetService<IScheduleRepository>();
            ScheduleDto schedule = await request.ReadFromJsonAsync<ScheduleDto>();
            var ID = service.Create(schedule);
            await context.Response.WriteAsJsonAsync(ID);
        });
        app.MapPut("/Schedule/{id}", async (long id, HttpContext context) =>
        {
            var request = context.Request;
            var service = context.RequestServices.GetService<IScheduleRepository>();
            ScheduleDto schedule = await request.ReadFromJsonAsync<ScheduleDto>();
            schedule.Id = id;
            var Id = service.Update(schedule);
            await context.Response.WriteAsJsonAsync(Id);
        });
        app.MapDelete("/Schedule/{id}", async (long id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IStudentRepository>();
            service.Delete(id);
        });
    }
}