using Volo.Abp.Modularity;

namespace Snooker;

[DependsOn(
    typeof(SnookerApplicationModule),
    typeof(SnookerDomainTestModule)
    )]
public class SnookerApplicationTestModule : AbpModule
{

}
