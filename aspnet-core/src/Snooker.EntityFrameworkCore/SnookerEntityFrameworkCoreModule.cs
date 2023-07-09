using Microsoft.Extensions.DependencyInjection;
using Snooker.Interclub.EntityFrameworkCore;
using Snooker.Platform.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Snooker.EntityFrameworkCore;

[DependsOn(
    typeof(InterclubEntityFrameworkCoreModule),
    typeof(PlatformEntityFrameworkCoreModule),
    typeof(SnookerDomainModule))]
public class SnookerEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<SnookerDbContext>();

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();

#if DEBUG
            options.Configure(configureOptions =>
            {
                configureOptions.UseSqlServer();
                configureOptions.DbContextOptions.EnableSensitiveDataLogging();
            });
#endif
        });
    }

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SnookerEfCoreEntityExtensionMappings.Configure();
    }
}