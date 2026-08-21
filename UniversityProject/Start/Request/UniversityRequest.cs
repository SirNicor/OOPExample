using IRepositoryAll;
using Logger;
using UCore;

namespace Start.Request;

public static class UniversityRequest
{
    public static void AddUniversityRequest(this IEndpointRouteBuilder app, MyLogger logger)
    {
        app.MapGet("/University/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<ReturnOneUniversity>();   
            var university = service.ReturnUniversity(id);
            await context.Response.WriteAsJsonAsync(university); 
        });
        app.MapGet("/University", async context =>
        {
            var service = context.RequestServices.GetService<ReturnListOfUniversity>();
            var universities = service.ReturnList();            
            await context.Response.WriteAsJsonAsync(universities);
        });
        app.MapPut("/University/{id}", async (int id, HttpContext context) =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IUniversityRepository>();
            UniversityDto university = await request.ReadFromJsonAsync<UniversityDto>();
            university.IdUniversity = id;
            var idUpdate = service.Update(university);
            await context.Response.WriteAsJsonAsync(idUpdate);
        });
        app.MapPost("/University", async context =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IUniversityRepository>();
            UniversityDto university = await request.ReadFromJsonAsync<UniversityDto>();
            var id = service.Create(university);
            await context.Response.WriteAsJsonAsync(id);
        });
        app.MapDelete("/University/{id}", async (long id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IUniversityRepository>();
            service.Delete(id);
        });
    }
}