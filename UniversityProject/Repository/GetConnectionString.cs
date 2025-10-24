namespace Repository;
using Microsoft.Extensions.Configuration;

public class GetConnectionString : IGetConnectionString
{
    public GetConnectionString(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ReturnConnectionString()
    {
        return _configuration.GetValue<string>("ConnectionStrings");
    }
    
    private IConfiguration _configuration;
}