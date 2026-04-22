using System.Drawing;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace Start;


public static class CreateJwtTokensService
{
    public static void CreateJwtTokens(this IServiceCollection service, IConfiguration configuration)
    {
        string key = configuration.GetSection("Auth:Key").Value;
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidAudience = configuration.GetSection("Auth:Audience").Value,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(key)),
                    ValidateIssuerSigningKey = true,
                };
            });
    }
}