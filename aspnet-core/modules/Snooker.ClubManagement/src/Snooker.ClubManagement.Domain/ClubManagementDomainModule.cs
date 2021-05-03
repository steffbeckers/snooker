using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Snooker.ClubManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(ClubManagementDomainSharedModule)
    )]
    public class ClubManagementDomainModule : AbpModule
    {

    }
}
