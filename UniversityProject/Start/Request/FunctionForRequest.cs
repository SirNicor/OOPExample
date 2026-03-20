using System.Text;
using UCore;
using Dadata;
using Dadata.Model;
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

    public async static Task<Address> CleanAddress(string address, IConfiguration configuration)
    { 
        var token = configuration.GetValue<string>("DaData:token");
        var secret =  configuration.GetValue<string>("DaData:secret");
        var api = new CleanClientAsync(token, secret);
        var result = await api.Clean<Address>(address);
        return result;
    }
    public async static Task<SuggestResponse<Address>> SuggestAddress(string address, IConfiguration configuration)
    { 
        var token = configuration.GetValue<string>("DaData:token");
        var api = new SuggestClientAsync(token);
        var result = await api.SuggestAddress(address);
        return result;
    }
}