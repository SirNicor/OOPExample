namespace Start.Middleware;
using Logger;

public class LoggingMiddleware
{
    readonly MyLogger _logger;
    readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next, MyLogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        _logger.Info($"BaseUrl = {request.Host}, path = {request.Path}," +
                     $"body: {request.Body}, contentType : {request.ContentType}");
        await _next(context);
        _logger.Info($"Request: path = {request.Path}," +
                     $"body: {context.Response.Body}, contentType : {context.Response.ContentType}");
    }
}