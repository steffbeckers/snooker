using Snooker.Platform;
using Volo.Abp.Modularity;

namespace Snooker.Interclub;

[DependsOn(
    typeof(InterclubDomainSharedModule),
    typeof(PlatformApplicationContractsModule))]
public class InterclubApplicationContractsModule : AbpModule
{
}