using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Snooker.EntityFrameworkCore
{
    [DependsOn(
        typeof(SnookerEntityFrameworkCoreModule)
        )]
    public class SnookerEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SnookerMigrationsDbContext>();
        }
    }
}
