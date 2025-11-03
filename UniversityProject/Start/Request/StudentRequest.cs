using Logger;

namespace Start;
using Repository;
using UCore;

static class StudentRequest
{
    public static void AddStudentRequest(this IEndpointRouteBuilder app, MyLogger logger, IConfiguration configuration)
    {
        app.MapGet("/Student/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<ReturnOneStudent>();   
            var student = service.ReturnStudent(id);
            student.PrintInfo(logger);
            await context.Response.WriteAsJsonAsync(student); 
        });
        app.MapGet("/Student", async context =>
        {
            var service = context.RequestServices.GetService<ReturnListOfStudents>();
            var students = service.ReturnList();            
            await context.Response.WriteAsJsonAsync(students);
        });
        app.MapPost("/Student", async context =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IStudentRepository>();
            Student student1 = await request.ReadFromJsonAsync<Student>();
            int ID = service.Create(student1);
            await context.Response.WriteAsJsonAsync(ID);
        });
        app.MapDelete("/Student/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IStudentRepository>();
            service.Delete(id);
        });
    }
}