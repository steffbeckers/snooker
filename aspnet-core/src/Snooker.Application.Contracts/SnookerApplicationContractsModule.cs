using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Snooker
{
    [DependsOn(
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpObjectExtendingModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(SnookerDomainSharedModule))]
    public class SnookerApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            SnookerDtoExtensions.Configure();
        }
    }
}