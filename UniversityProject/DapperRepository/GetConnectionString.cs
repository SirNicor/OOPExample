namespace Repository;
using Microsoft.Extensions.Configuration;
using IRepositoryAll;

public class GetConnectionString(IConfiguration configuration) : IGetConnectionString
{
    public string ReturnConnectionString()
    {
        return configuration.GetValue<string>("ConnectionStrings");
    }
}