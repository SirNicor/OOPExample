using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Start.Middleware;
using Logger;

public class LoggingMiddleware
{
    readonly MyLogger _logger;
    readonly RequestDelegate _next;
    readonly IConfiguration _configuration;

    public LoggingMiddleware(RequestDelegate next, MyLogger logger, IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        _logger.Info($"Request baseUrl = {request.Host}, path = {request.Path}," +
                     $"contentType : {request.ContentType}, method =  {request.Method}");
        await _next(context);
        _logger.Info($"Request: path = {request.Path}, contentType : {context.Response.ContentType}");
    }
}