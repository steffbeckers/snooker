using Snooker.Interclub;
using Snooker.Platform;
using Volo.Abp.Modularity;

namespace Snooker;

[DependsOn(
    typeof(InterclubDomainSharedModule),
    typeof(PlatformDomainSharedModule))]
public class SnookerDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SnookerGlobalFeatureConfigurator.Configure();
        SnookerModuleExtensionConfigurator.Configure();
    }
}