using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Snooker.ClubManagement
{
    [DependsOn(
        typeof(ClubManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class ClubManagementApplicationContractsModule : AbpModule
    {

    }
}
