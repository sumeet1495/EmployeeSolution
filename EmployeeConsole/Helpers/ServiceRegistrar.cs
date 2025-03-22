using EmployeeLibrary.Database;
using EmployeeLibrary.Interfaces.Database;
using EmployeeLibrary.Interfaces.Repositories;
using EmployeeLibrary.Interfaces.Services;
using EmployeeLibrary.Repositories;
using EmployeeLibrary.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeConsole.Helpers
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection RegisterServices(IConfiguration config)
        {
            var services = new ServiceCollection();

            // Logging adding it to console and also defines the levels from appsetting.json
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddConsole();
            });

            // Connection string
            string connectionString = config.GetConnectionString("PostgresConnection");

            // DB Factory for connection amd making singleton for shared instance
            services.AddSingleton<IDbConnectionFactory>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DbConnectionFactory>>();
                return new DbConnectionFactory(connectionString, logger);
            });

            // Repositories registration
            services.AddSingleton<IEmployeeAdderRepository, EmployeeAdderRepository>();
            services.AddSingleton<IEmployeeFetcherRepository, EmployeeFetcherRepository>();

            // Services registration
            services.AddSingleton<IEmployeeAdderService, EmployeeAdderService>();
            services.AddSingleton<IEmployeeFetcherService, EmployeeFetcherService>();

            // App class registration in services container
            services.AddSingleton<App>();

            return services;
        }
    }
}

