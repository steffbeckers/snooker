using Volo.Abp.Modularity;

namespace Snooker.Platform;

[DependsOn(
    typeof(PlatformApplicationModule),
    typeof(PlatformDomainTestModule)
    )]
public class PlatformApplicationTestModule : AbpModule
{

}
