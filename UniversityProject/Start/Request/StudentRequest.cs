using System.Globalization;
using System.Reflection;
using System.Text.Json;
using Logger;
using Microsoft.AspNetCore.Mvc;

namespace Start;
using Repository;
using UCore;
using System.Linq.Dynamic.Core;
static class StudentRequest
{
    public static void AddStudentRequest(this IEndpointRouteBuilder app, MyLogger logger, IConfiguration configuration)
    {
        app.MapGet("/Student/{studentId}", async (long studentId, HttpContext context) =>
        {
            logger.Info($"/student/{studentId}");
            var service = context.RequestServices.GetService<IStudentRepository>();   
            var student = service.GetStudentPage(studentId);
            await context.Response.WriteAsJsonAsync(student); 
        });
        app.MapGet("/Student/Page/{count}",async (int count, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IStudentRepository>();
            var allCount = service.GetCount();
            var countOfPage = allCount / count +  (allCount % count == 0? 0: 1);
            logger.Info($"student/Page/{count} = > {countOfPage}");
            await context.Response.WriteAsJsonAsync(countOfPage);
        });
        app.MapGet("/Student", async(string? filter, string? sortKey, string? sortOrder, int page, int count, HttpContext context) =>
        {
            logger.Info($"get student/{sortKey} {sortOrder} {page} {count} {filter}");
            FilterDto filterDto = JsonSerializer.Deserialize<FilterDto>(filter);
            int firstId = (page-1) * count;
            var service = context.RequestServices.GetService<IStudentRepository>();
            var studentAndPage = service.GetStudentTableDTO(firstId, count, sortKey,
                sortOrder, filterDto);
            var allCount = studentAndPage.Item2;
            var countOfPage = allCount / count +  (allCount % count == 0? 0: 1);
            logger.Info($"student/{page} {count} {firstId} {sortKey} {sortOrder}");
            await context.Response.WriteAsJsonAsync(new Tuple<List<StudentTableDTO>, long>(studentAndPage.Item1, countOfPage));
        });
        app.MapPost("/Student", async context =>
        {
            logger.Info($"/student POST");
            var request = context.Request;
            var service =  context.RequestServices.GetService<IStudentRepository>();
            var student = await request.ReadFromJsonAsync<StudentDtoForPage>();
            var ID = service.Create(student);
            await context.Response.WriteAsJsonAsync(ID);
        });
        app.MapPut("/Student/{id}", async (long id, HttpContext context) =>
        {
            logger.Info($"put Student/{id}");
            var request = context.Request;
            var service = context.RequestServices.GetService<IStudentRepository>();
            StudentDtoForPage student = await request.ReadFromJsonAsync<StudentDtoForPage>();
            student.studentId = id;
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