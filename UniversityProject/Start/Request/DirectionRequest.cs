namespace Start;
using Logger;
using Repository;
using UCore;

static class DirectionRequest
{
    public static void AddDirectionRequest(this IEndpointRouteBuilder app, MyLogger logger)
    {
        app.MapGet("/Direction/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IDepartmentRepository>();   
            var department = service.Get(id);
            await context.Response.WriteAsJsonAsync(department); 
        });
        app.MapGet("/Direction", async context =>
        {
            var service = context.RequestServices.GetService<IDepartmentRepository>();
            var departments = service.ReturnList();            
            await context.Response.WriteAsJsonAsync(departments);
        });
    }
}