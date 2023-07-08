using Snooker.Interclub;
using Snooker.Platform;
using Volo.Abp.Modularity;

namespace Snooker;

[DependsOn(
    typeof(InterclubHttpApiModule),
    typeof(PlatformHttpApiModule),
    typeof(SnookerApplicationContractsModule))]
public class SnookerHttpApiModule : AbpModule
{
}