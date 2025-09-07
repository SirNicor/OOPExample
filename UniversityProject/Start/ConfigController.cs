using Logger;

public class ConfigController
{
    public ConfigController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<string> Get()
    {
        var DefaultLogging = _configuration.GetValue<string>("Logging:LogLevel:Default");
        var TypeLogging = _configuration.GetValue<string>("Logging:TypeLogger");
        return new string[] { DefaultLogging, TypeLogging };
    }

    private readonly IConfiguration _configuration;
}