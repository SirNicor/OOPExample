namespace Repository;
using Microsoft.Extensions.Configuration;

public class GetDBPath : IGetDBPath
{
    public GetDBPath(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ReturnPath()
    {
        return _configuration.GetValue<string>("ConnectionStrings");
    }
    
    private IConfiguration _configuration;
}