using Snooker.Interclub;
using Snooker.Platform;
using Volo.Abp.Modularity;

namespace Snooker;

[DependsOn(
    typeof(InterclubApplicationModule),
    typeof(PlatformApplicationModule),
    typeof(SnookerDomainModule),
    typeof(SnookerApplicationContractsModule))]
public class SnookerApplicationModule : AbpModule
{
}