using Snooker.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Snooker.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SnookerApplicationContractsModule),
    typeof(SnookerEntityFrameworkCoreModule))]
public class SnookerDbMigratorModule : AbpModule
{
}