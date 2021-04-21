using Volo.Abp.Modularity;

namespace Snooker.ClubManagement
{
    [DependsOn(
        typeof(ClubManagementApplicationModule),
        typeof(ClubManagementDomainTestModule)
        )]
    public class ClubManagementApplicationTestModule : AbpModule
    {

    }
}
