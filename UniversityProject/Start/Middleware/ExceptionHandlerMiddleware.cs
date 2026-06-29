using Logger;

namespace Start.Middleware;

public class ExceptionHandlerMiddleware
{
    readonly MyLogger _logger;
    readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next, MyLogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message + " " +ex.StackTrace + Environment.NewLine + "Source =" + ex.Source);
            if (!context.Response.HasStarted)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}