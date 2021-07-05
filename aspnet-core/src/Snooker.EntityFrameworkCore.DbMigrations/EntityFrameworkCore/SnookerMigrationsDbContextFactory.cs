using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Snooker.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */

    public class SnookerMigrationsDbContextFactory : IDesignTimeDbContextFactory<SnookerMigrationsDbContext>
    {
        public SnookerMigrationsDbContext CreateDbContext(string[] args)
        {
            SnookerEfCoreEntityExtensionMappings.Configure();

            IConfigurationRoot configuration = BuildConfiguration();

            DbContextOptionsBuilder<SnookerMigrationsDbContext> builder = new DbContextOptionsBuilder<SnookerMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new SnookerMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Snooker.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}