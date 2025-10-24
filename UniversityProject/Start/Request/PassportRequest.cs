namespace Start;
using Repository;
using UCore;
using Logger;

static class PassportRequest
{
    public static void AddPassportRequest(this IEndpointRouteBuilder app, MyLogger logger)
    {
        app.MapDelete("/Passport/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IStudentRepository>();
            service.DeletePassport(id);
        });
    }
}