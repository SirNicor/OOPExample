using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UCore;
using Dadata;
using Dadata.Model;
using Microsoft.IdentityModel.Tokens;
using Address = Dadata.Model.Address;

namespace Start;

public static class FunctionForRequest
{
    public static StringBuilder PathReturn(string sortKey, Type typeOfClass, StringBuilder path = null)
    {
        if (path == null)
        {
            path = new StringBuilder("");
        }
        foreach (var field in typeOfClass.GetProperties())
        {
            if (string.Equals(field.Name, sortKey, StringComparison.OrdinalIgnoreCase))
            {
                path.Append(field.Name);
                return path;
            }

            if (!field.PropertyType.IsPrimitive && field.PropertyType != typeof(string)
                                                && field.PropertyType != typeof(DateTime) &&
                                                !field.PropertyType.IsValueType)
            {
                path.Append($"{field.Name}.");
                path = PathReturn(sortKey, field.PropertyType, path);
                if (path.Length != 0)
                {
                    return path;
                }
            }
        }

        path.Length = 0;
        return path;
    }

    public static async Task<Address> CleanAddress(string address, IConfiguration configuration)
    { 
        var token = configuration.GetValue<string>("DaData:token");
        var secret =  configuration.GetValue<string>("DaData:secret");
        var api = new CleanClientAsync(token, secret);
        var result = await api.Clean<Address>(address);
        return result;
    }
    public static async Task<SuggestResponse<Address>> SuggestAddress(string address, IConfiguration configuration)
    { 
        var token = configuration.GetValue<string>("DaData:token");
        var api = new SuggestClientAsync(token);
        var result = await api.SuggestAddress(address);
        return result;
    }
    public static IResult AttachAccountToContext(string token, IConfiguration configuration)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(configuration["Auth:Key"]);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = true,
                ValidAudience = configuration["Auth:AUDIENCE"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return Results.Ok();
        }
        catch (SecurityTokenExpiredException)
        {
            return Results.Json(new{message = "SendRefresh"}, statusCode: 401);
        }
        catch (SecurityTokenInvalidSignatureException)
        {
            return Results.Json(new{message = "ResetAut"}, statusCode: 401);
        }
        catch (Exception ex)
        {
            return Results.Json(new{message = "Error"}, statusCode: 400);
        }
    }
}