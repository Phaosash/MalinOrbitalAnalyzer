using Microsoft.Extensions.Logging;

namespace ErrorLogging;

public class LoggingHandler {
    private readonly ILogger<LoggingHandler>? _logger;
    private static LoggingHandler? _instance;
    private static readonly object _lock = new();

    private LoggingHandler(ILogger<LoggingHandler> logger){
        _logger = logger;
    }

    public static LoggingHandler Instance {
        get {
            if (_instance == null){
                lock (_lock){
                    if (_instance == null){
                        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                        var logger = loggerFactory.CreateLogger<LoggingHandler>();
                        _instance = new LoggingHandler(logger);
                    }
                }
            }

            return _instance;
        }
    }

    public void LogError (string msg, Exception ex){
        _logger!.LogError("Error: {Message}, Exception: {Exception}", msg, ex);
    }

    public void LogInformation (string msg){
        _logger!.LogInformation("Information: {Message}", msg);
    }

    public void LogWarning (string msg){ 
        _logger!.LogWarning("Warning: {Message}", msg);
    }
}