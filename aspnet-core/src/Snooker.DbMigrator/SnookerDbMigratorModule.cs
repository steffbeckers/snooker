using Snooker.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Snooker.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SnookerEntityFrameworkCoreModule),
    typeof(SnookerApplicationContractsModule)
    )]
public class SnookerDbMigratorModule : AbpModule
{

}
