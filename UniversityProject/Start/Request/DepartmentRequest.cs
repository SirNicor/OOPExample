namespace Start;
using Logger;
using Repository;
using UCore;

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
            var ID = service.Update(department);
            await context.Response.WriteAsJsonAsync(ID);
        });
        app.MapPost("/Department", async context =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IDepartmentRepository>();
            DepartmentDto department = await request.ReadFromJsonAsync<DepartmentDto>();
            var ID = service.Create(department);
            await context.Response.WriteAsJsonAsync(ID);
        });
        app.MapDelete("/Department/{id}", async (long id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IDepartmentRepository>();
            service.Delete(id);
        });
    }
}