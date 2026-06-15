using System.IdentityModel.Tokens.Jwt;
using Logger;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace Start.Middleware;

public class AuthenticationMiddleware
{
    readonly RequestDelegate _next;
    readonly IConfiguration _configuration;
    readonly MyLogger _logger;

    public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration,  MyLogger logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.ToString();
        if (path.ToLower() != "/authorization" && path.ToLower() != "/login" && path.ToLower() != "/resetaccesstoken" && path != "/")
        {
            context.Request.Headers.TryGetValue("authorization", out var token);
            if (token.ToString() == null)
            {
                await SendBadRequest(context, 401, "ResetAut");
                return;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Convert.FromBase64String(_configuration["Auth:Key"]);
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidAudience = _configuration["Auth:AUDIENCE"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                context.User = claimsPrincipal;
                foreach (var x in claimsPrincipal.Claims)
                {
                    _logger.Info($"Claims - {x.Type};  {x.Value}");
                }
            }
            catch (SecurityTokenExpiredException)
            {
                await SendBadRequest(context, 401, "SendRefresh");
                return;
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                await SendBadRequest(context, 401, "ResetAut");
                return;
            }
            catch (Exception ex)
            {
                await SendBadRequest(context, 400, "Error");
                return;
            }
        }
        await _next(context);
    }

    public static async Task SendBadRequest(HttpContext context, int statusCode, string message)
    {
        if (context.Response.HasStarted)
        {
            return;
        }
        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(message);
    }
}