using Microsoft.Extensions.Configuration;

namespace ApiTelegramBot;

public class GetToken : IGetToken
{
    private IConfiguration _configuration;
    public GetToken(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string ReturnToken()
    {
        return _configuration["Token"];
    }
}