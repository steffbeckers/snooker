using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Snooker.Interclub;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(InterclubDomainSharedModule)
)]
public class InterclubDomainModule : AbpModule
{

}
