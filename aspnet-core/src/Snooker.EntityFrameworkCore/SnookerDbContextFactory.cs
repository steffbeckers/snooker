using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Snooker.EntityFrameworkCore;

public class SnookerDbContextFactory : IDesignTimeDbContextFactory<SnookerDbContext>
{
    public SnookerDbContext CreateDbContext(string[] args)
    {
        SnookerEfCoreEntityExtensionMappings.Configure();

        IConfigurationRoot configuration = BuildConfiguration();

        DbContextOptionsBuilder<SnookerDbContext> builder = new DbContextOptionsBuilder<SnookerDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new SnookerDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Snooker.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}