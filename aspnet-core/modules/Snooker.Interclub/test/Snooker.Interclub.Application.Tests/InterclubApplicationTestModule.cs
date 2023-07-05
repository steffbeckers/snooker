using Volo.Abp.Modularity;

namespace Snooker.Interclub;

[DependsOn(
    typeof(InterclubApplicationModule),
    typeof(InterclubDomainTestModule)
    )]
public class InterclubApplicationTestModule : AbpModule
{

}
