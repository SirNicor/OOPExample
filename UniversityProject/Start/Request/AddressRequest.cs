namespace Start;
using Repository;
using UCore;
using Logger;
using Dadata.Model;
using IRepositoryAll;

static class AddressRequest
{
    public static void AddAddressRequest(this IEndpointRouteBuilder app, MyLogger logger, IConfiguration config)
    {
        app.MapDelete("/Address/{id}", async (int id, HttpContext context) =>
        {
            var service = context.RequestServices.GetService<IStudentRepository>();
            service.DeleteAddress(id);
        });
        app.MapGet("/Address/Suggest/{address}", async (string address, HttpContext context) =>
        {
            var suggest = FunctionForRequest.SuggestAddress(address, config).Result;
            logger.Info($"Suggest {suggest.suggestions}");
            await context.Response.WriteAsJsonAsync(suggest.suggestions);
        });
        app.MapGet("/Address/Clean/{address}", async (string address, HttpContext context) =>
        {
            var clean = FunctionForRequest.CleanAddress(address, config).Result;
            await context.Response.WriteAsJsonAsync(clean);
        });
    }
}