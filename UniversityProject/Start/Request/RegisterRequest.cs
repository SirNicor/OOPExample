using IRepositoryAll;
using Logger;
using UCore;

namespace Start.Request;

static class RegisterRequest
{
    public static void AddRegisterRequest(this IEndpointRouteBuilder app, MyLogger logger)
    {
        app.MapPost("/Register", async context =>
        {
            var request = context.Request;
            var service = context.RequestServices.GetService<IRegistrationRepository>();
            RegistrationDTO registration = await request.ReadFromJsonAsync<RegistrationDTO>();
            var id = service.Create(registration);
            await context.Response.WriteAsJsonAsync(id);
        });
    }
}