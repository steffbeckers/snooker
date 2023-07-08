using Snooker.Interclub;
using Snooker.Platform;
using Volo.Abp.Modularity;

namespace Snooker;

[DependsOn(
    typeof(InterclubApplicationModule),
    typeof(PlatformApplicationModule),
    typeof(SnookerApplicationContractsModule),
    typeof(SnookerDomainModule))]
public class SnookerApplicationModule : AbpModule
{
}