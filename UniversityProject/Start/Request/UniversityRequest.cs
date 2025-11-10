namespace Start;
using Logger;
using Repository;
using UCore;
static public class UniversityRequest
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
            UniversityForDB university = await request.ReadFromJsonAsync<UniversityForDB>();
            Tuple<int, UniversityForDB> idAndUniversity = new Tuple<int, UniversityForDB>(id,  university);
            int ID = service.Update(idAndUniversity);
            await context.Response.WriteAsJsonAsync(ID);
        });
        app.MapPost("/University", async context =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IUniversityRepository>();
            UniversityForDB university = await request.ReadFromJsonAsync<UniversityForDB>();
            int ID = service.Create(university);
            await context.Response.WriteAsJsonAsync(ID);
        });
        app.MapDelete("/University/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IUniversityRepository>();
            service.Delete(id);
        });
    }
}