namespace Start;
using Logger;

static class OtherRequest
{
    public static void AddOtherRequest(this IEndpointRouteBuilder app, MyLogger logger, IConfiguration configuration)
    {
        app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
            string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
    }
}