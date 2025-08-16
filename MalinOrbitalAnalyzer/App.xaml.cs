using MalinOrbitalAnalyzer.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MalinOrbitalAnalyzer;

public partial class App : Application {
    public static IServiceProvider? ServiceProvider { get; private set; }

    protected override void OnStartup (StartupEventArgs e){
        base.OnStartup(e);

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private static void ConfigureServices (IServiceCollection services){
        services.AddSingleton<CentralizedErrorLogger>();
        services.AddSingleton<MainWindow>();
    }
}