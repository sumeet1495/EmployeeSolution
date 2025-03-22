using EmployeeConsole;
using EmployeeConsole.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

try
{
    // Loading the configuration from appsettings.json
    var config = ConfigurationSetup.Load();

    // Registering all the services for the app present under Helpers layer
    var services = ServiceRegistrar.RegisterServices(config);

    // Building the service provider for execution
    var provider = services.BuildServiceProvider();

    // Resolving and running the app
    var app = provider.GetRequiredService<App>();
    await app.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine("An unexpected error occurred while starting the application.");
    Console.WriteLine($"Error: {ex.Message}");
}