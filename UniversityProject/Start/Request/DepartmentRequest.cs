using IRepositoryAll;
using Logger;
using UCore;

namespace Start.Request;

static class DepartmentRequest
{
    public static void AddDepartmentRequest(this IEndpointRouteBuilder app, MyLogger logger)
    {
        app.MapGet("/Department/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IDepartmentRepository>();   
            var department = service.Get(id);
            await context.Response.WriteAsJsonAsync(department); 
        });
        app.MapGet("/Department", async context =>
        {
            var service = context.RequestServices.GetService<IDepartmentRepository>();
            var departments = service.ReturnList();            
            await context.Response.WriteAsJsonAsync(departments);
        });
        app.MapPut("/Department/{id}", async (long id, HttpContext context) =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IDepartmentRepository>();
            DepartmentDto department = await request.ReadFromJsonAsync<DepartmentDto>();
            department.DepartmentId = id;
            var idUpdate = service.Update(department);
            await context.Response.WriteAsJsonAsync(idUpdate);
        });
        app.MapPost("/Department", async context =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IDepartmentRepository>();
            DepartmentDto department = await request.ReadFromJsonAsync<DepartmentDto>();
            var id = service.Create(department);
            await context.Response.WriteAsJsonAsync(id);
        });
        app.MapDelete("/Department/{id}", (long id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IDepartmentRepository>();
            service.Delete(id);
            return Task.CompletedTask;
        });
    }
}