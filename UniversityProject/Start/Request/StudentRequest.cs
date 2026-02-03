using System.Globalization;
using System.Reflection;
using Logger;

namespace Start;
using Repository;
using UCore;
using System.Linq.Dynamic.Core;
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
            logger.Info("https://localhost:7082/api/students");
            await context.Response.WriteAsJsonAsync(students);
        });
        app.MapGet("/Student/{sortKey}/{sortOrder}", async(string sortKey, string sortOrder, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<ReturnListOfStudents>();
            var students = service.ReturnList().AsQueryable();
            var path = FunctionForRequest.PathReturn(sortKey, typeof(Student));
            var sortStudents = new List<Student>();
            string direction;
            if (path.Length == 0)
            {
                sortStudents = students.ToList();
            }
            else
            {
                if (sortOrder == "AscendingOrder")
                {
                    direction = "ASC";
                    sortStudents = students.OrderBy($"{path} {direction}").ToList();
                }
                else
                {
                    direction = "DESC";
                    sortStudents = students.OrderBy($"{path} {direction}").ToList();
                }
            }
            logger.Info("https://localhost:7082/api/students");
            await context.Response.WriteAsJsonAsync(sortStudents);
        });
        app.MapPost("/Student", async context =>
        {
            var request = context.Request;
            var service =  context.RequestServices.GetService<IStudentRepository>();
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