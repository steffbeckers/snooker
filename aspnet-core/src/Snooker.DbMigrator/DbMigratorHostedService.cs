using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Snooker.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;

namespace Snooker.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (IAbpApplicationWithInternalServiceProvider application = AbpApplicationFactory.Create<SnookerDbMigratorModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());

                // Not added by ABP.io framework
                options.Services.ReplaceConfiguration(BuildConfiguration());
            }))
            {
                application.Initialize();

                await application
                    .ServiceProvider
                    .GetRequiredService<SnookerDbMigrationService>()
                    .MigrateAsync();

                application.Shutdown();

                _hostApplicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        // Not added by ABP.io framework
        private static IConfiguration BuildConfiguration()
        {
            string environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .Build();
        }
    }
}