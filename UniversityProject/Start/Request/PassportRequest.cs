using IRepositoryAll;
using Logger;

namespace Start.Request;

static class PassportRequest
{
    public static void AddPassportRequest(this IEndpointRouteBuilder app, MyLogger logger)
    {
        app.MapDelete("/Passport/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IStudentRepository>();
            await service.DeletePassportAsync(id);
        });
    }
}