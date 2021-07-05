using Snooker.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Snooker.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(SnookerApplicationContractsModule),
        typeof(SnookerEntityFrameworkCoreDbMigrationsModule))]
    public class SnookerDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}