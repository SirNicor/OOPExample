using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Telegram.Bot.Types;
using UCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Logger;

namespace Start;

public static class AuthAndLoginRequest
{
    public static void AddAuthAndLoginRequest(this IEndpointRouteBuilder app, IConfiguration configuration,
        MyLogger logger)
    {
        app.MapPost("/Authorization", async (HttpContext ctx) =>
        {
            logger.Info("@/Authorization");
            var authRep = ctx.RequestServices.GetService<IAuthorizationRepository>();
            var user = await ctx.Request.ReadFromJsonAsync<AuthorizationDto>();
            long id = authRep.CreateAuthorization(user);
            await ctx.Response.WriteAsJsonAsync(id);
        });
        app.MapPost("/Login", async (HttpContext ctx, AuthorizationForGetJwtToken dto) =>
        {
            logger.Info("@/Login");
            var authAndLoginRep = ctx.RequestServices.GetService<IAuthorizationRepository>();
            var roleRep = ctx.RequestServices.GetService<IRoleRepository>();
            AuthorizationForGetJwtToken? auth = dto;
            var userIdAndRole = authAndLoginRep.GetAuthorizationsRoleForIndex(auth);
            var userId = userIdAndRole.Item1;
            var rolesId =  userIdAndRole.Item2;
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            bool checkPassword = authAndLoginRep.CheckPassword(auth.Password, (long)userId);
            if (!checkPassword)
            {
                return Results.Unauthorized();
            }

            var roles = roleRep.GetRoleAccess((int[])rolesId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, auth.Login), 
                new Claim(ClaimTypes.Email, auth.Email),
                new Claim(ClaimTypes.MobilePhone, auth.Phone),
            };
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.NameRole));
            }
            string key = configuration.GetSection("Auth:Key").Value;
            var Accessjwt = new JwtSecurityToken(
                audience: configuration.GetSection("Auth:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.Add(
                    TimeSpan.FromMinutes(Convert.ToInt64(configuration.GetSection("Auth:TimeAccessJwtToken").Value))),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(key)),
                    SecurityAlgorithms.HmacSha256));
            claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, auth.Id.ToString()) };
            var refreshJwt = new JwtSecurityToken(
                audience: configuration.GetSection("Auth:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.Add(
                    TimeSpan.FromMinutes(Convert.ToInt64(configuration.GetSection("Auth:TimeRefreshJwtToken").Value))),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(key)),
                    SecurityAlgorithms.HmacSha256));
            RefreshJWTTokenDTO refreshJwtDto = new RefreshJWTTokenDTO();
            refreshJwtDto.Token = new JwtSecurityTokenHandler().WriteToken(refreshJwt);
            refreshJwtDto.RevokedAt = false;
            refreshJwtDto.IdAuthorizationTable = (long)userId;
            authAndLoginRep.CreateJWTToken(refreshJwtDto);
            return Results.Ok(new
            {
                Accessjwt = new JwtSecurityTokenHandler().WriteToken(Accessjwt),
                Refreshjwt = new JwtSecurityTokenHandler().WriteToken(refreshJwt),
                Role = JsonSerializer.Serialize(roles)
            });
        });
        app.MapGet("/ResetAccessToken", async (HttpContext ctx) =>
        {
            logger.Info("@/ResetAccessToken");
            var request = ctx.Request;
            request.Headers.TryGetValue("authorization", out var token);
            var authAndLoginRep = ctx.RequestServices.GetService<IAuthorizationRepository>();
            var ver = authAndLoginRep.CheckAndUpdateJWTToken(token);
            if (ver is null)
            {
                return Results.Unauthorized();
            }

            var auth = authAndLoginRep.GetForIdAuthorization((long)ver);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, auth.Login), new Claim(ClaimTypes.Email, auth.Email),
                new Claim(ClaimTypes.MobilePhone, auth.Phone)
            };
            string key = configuration.GetSection("Auth:Key").Value;
            var Accessjwt = new JwtSecurityToken(
                audience: configuration.GetSection("Auth:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.Add(
                    TimeSpan.FromMinutes(Convert.ToInt64(configuration.GetSection("Auth:TimeAccessJwtToken").Value))),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(key)),
                    SecurityAlgorithms.HmacSha256));
            claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, auth.Id.ToString()) };
            var refreshJwt = new JwtSecurityToken(
                audience: configuration.GetSection("Auth:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.Add(
                    TimeSpan.FromMinutes(Convert.ToInt64(configuration.GetSection("Auth:TimeRefreshJwtToken").Value))),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(key)),
                    SecurityAlgorithms.HmacSha256));
            RefreshJWTTokenDTO refreshJwtDto = new RefreshJWTTokenDTO();
            refreshJwtDto.Token = new JwtSecurityTokenHandler().WriteToken(refreshJwt);
            refreshJwtDto.RevokedAt = false;
            refreshJwtDto.IdAuthorizationTable = (long)auth.Id;
            authAndLoginRep.CreateJWTToken(refreshJwtDto);
            return Results.Ok(new
            {
                Accessjwt = new JwtSecurityTokenHandler().WriteToken(Accessjwt),
                Refreshjwt = new JwtSecurityTokenHandler().WriteToken(refreshJwt)
            });
        });
        app.MapGet("/CheckAccessToken", async (HttpContext ctx) =>
        {
            var request = ctx.Request;
            request.Headers.TryGetValue("authorization", out var token);
            logger.Info("@/CheckAccessToken " + token);
            IResult result = FunctionForRequest.AttachAccountToContext(token[0], configuration);
            if (result is IStatusCodeHttpResult statusResult && statusResult.StatusCode != 200)
            {
                return result;
            }

            return Results.Ok();
        });
    }
}