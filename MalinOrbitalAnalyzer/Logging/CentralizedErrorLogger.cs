using Microsoft.Extensions.Logging;

namespace MalinOrbitalAnalyzer.Logging;

internal class CentralizedErrorLogger {
    private readonly ILogger<CentralizedErrorLogger>? _logger;
    private static CentralizedErrorLogger? _instance;
    private static readonly object _lock = new();

    private CentralizedErrorLogger(ILogger<CentralizedErrorLogger> logger){
        _logger = logger;
    }

    public static CentralizedErrorLogger Instance {
        get {
            if (_instance == null){
                lock (_lock){
                    if (_instance == null){
                        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                        var logger = loggerFactory.CreateLogger<CentralizedErrorLogger>();
                        _instance = new CentralizedErrorLogger(logger);
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