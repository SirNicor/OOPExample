namespace Start;
using Repository;
using UCore;
using Logger;

static class AddressRequest
{
    public static void AddAddressRequest(this IEndpointRouteBuilder app, MyLogger logger)
    {
        app.MapDelete("/Address/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IStudentRepository>();
            service.DeleteAddress(id);
        });
    }
}