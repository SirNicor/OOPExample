namespace Logger;
using Microsoft.Extensions.Configuration;

public class ConfigurationLogger
{
    public ConfigurationLogger(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public MyLogger Get()
    {
        var defaultLogging = _configuration.GetValue<string>("Logging:LogLevel:Default");
        var typeLogging = _configuration.GetValue<string>("Logging:TypeLogger");
        switch (typeLogging)
        {
            case "Console":
                logger = new ConsoleMyLogger();
                break;
            case "File":
                logger = new FileMyLogger();
                break;
            case "All":
                logger = new AllMyLogger( new MyLogger[] {new ConsoleMyLogger(),  new FileMyLogger()});
                break;
            default:
                logger = new FileMyLogger();
                break;
        }

        switch(defaultLogging)
        {
            case "Information":
                logger.MinLog = LevelLoger.INFO;
                break;
            case "Warning":
                logger.MinLog = LevelLoger.WARNING;
                break;
            case "Error":
                logger.MinLog = LevelLoger.ERROR;
                break;
            case "Debug":
                logger.MinLog = LevelLoger.DEBUG;
                break;
            case "Fatal":
                logger.MinLog = LevelLoger.FATAL;
                break;
        }
        
        return logger;
    }

    private MyLogger logger;
    private readonly IConfiguration _configuration;
}