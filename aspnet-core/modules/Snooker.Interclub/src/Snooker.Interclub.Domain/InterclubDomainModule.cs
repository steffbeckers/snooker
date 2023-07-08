using Snooker.Platform;
using Volo.Abp.Modularity;

namespace Snooker.Interclub;

[DependsOn(
    typeof(InterclubDomainSharedModule),
    typeof(PlatformDomainModule))]
public class InterclubDomainModule : AbpModule
{
}