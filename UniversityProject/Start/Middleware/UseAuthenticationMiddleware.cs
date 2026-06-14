namespace Start.Middleware;

public static class UseAuthentication
{
    public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder  app)
    {
        return app.UseMiddleware<AuthenticationMiddleware>();
    }
}