using Snooker.Interclub;
using Snooker.Platform;
using Volo.Abp.Modularity;

namespace Snooker;

[DependsOn(
    typeof(InterclubDomainModule),
    typeof(PlatformDomainModule),
    typeof(SnookerDomainSharedModule))]
public class SnookerDomainModule : AbpModule
{
}