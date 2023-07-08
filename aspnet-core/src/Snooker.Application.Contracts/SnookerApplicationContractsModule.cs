using Snooker.Interclub;
using Snooker.Platform;
using Volo.Abp.Modularity;

namespace Snooker;

[DependsOn(
    typeof(InterclubApplicationContractsModule),
    typeof(PlatformApplicationContractsModule),
    typeof(SnookerDomainSharedModule))]
public class SnookerApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SnookerDtoExtensions.Configure();
    }
}