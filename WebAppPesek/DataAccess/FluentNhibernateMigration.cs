using System;
using System.Collections.Generic;
using System.Linq;
using FluentMigrator;
using FluentMigrator.Runner.Initialization;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public class FluentNhibernateMigration
    {
        public static void MigrateSchema()
        {
            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSqlServer2014()
                    // Set the connection string
                    .WithGlobalConnectionString("Data Source=DESKTOP-A5PR65K;Initial Catalog=tnpw2;User Id=sa;Password=heslo1234;")
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(AddLogTable).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
