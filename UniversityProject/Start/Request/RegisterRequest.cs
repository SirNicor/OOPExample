namespace Start;
using Repository;
using UCore;
using Logger;

static class RegisterRequest
{
    public static void AddRegisterRequest(this IEndpointRouteBuilder app, MyLogger logger)
    {
        app.MapPost("/Register", async context =>
        {
            var request = context.Request;
            var service = context.RequestServices.GetService<IRegistrationRepository>();
            RegistrationDTO registration = await request.ReadFromJsonAsync<RegistrationDTO>();
            var ID = service.Create(registration);
            await context.Response.WriteAsJsonAsync(ID);
        });
    }
}