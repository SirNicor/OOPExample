using Logger;
using Microsoft.AspNetCore.Diagnostics;

namespace Start.Middleware;

public static class UseExceptionHandler
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder  app)
    {
        return app.UseMiddleware<LoggingMiddleware>();
    }
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder  app)
    {
        return app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}