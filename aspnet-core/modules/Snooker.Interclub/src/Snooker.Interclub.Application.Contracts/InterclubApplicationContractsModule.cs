using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Snooker.Interclub;

[DependsOn(
    typeof(InterclubDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class InterclubApplicationContractsModule : AbpModule
{

}
