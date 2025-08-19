using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using ErrorLogging;

namespace MalinOrbitalAnalyzer;

public partial class App : Application {
    public static IServiceProvider? ServiceProvider { get; private set; }

    //  This method is called when the application starts.
    //  It first calls the base class’s OnStartup method, then sets up dependency injection by creating a
    //  ServiceCollection and configuring services through the ConfigureServices method.
    //  After configuring the services, it builds a ServiceProvider which resolves and provides instances of services.
    //  The MainWindow is then retrieved from the service provider and shown to the user, marking the application's
    //  main window as visible. This method helps initialize and set up services for the application at startup.
    protected override void OnStartup (StartupEventArgs e){
        base.OnStartup(e);

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    //  This method is responsible for configuring the application's dependency injection container.
    //  It registers two services: LoggingHandler and MainWindow, both as singletons.
    //  This means that only one instance of each service will be created and shared throughout the
    //  application's lifecycle. By adding these services to the IServiceCollection, they can later
    //  be resolved and injected into other parts of the application, enabling better management and
    //  decoupling of dependencies.
    private static void ConfigureServices (IServiceCollection services){
        services.AddSingleton<LoggingHandler>();
        services.AddSingleton<MainWindow>();
    }
}