using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Snooker.ClubManagement;

namespace Snooker
{
    [DependsOn(
        typeof(SnookerDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule)
    )]
    [DependsOn(typeof(ClubManagementApplicationContractsModule))]
    public class SnookerApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            SnookerDtoExtensions.Configure();
        }
    }
}