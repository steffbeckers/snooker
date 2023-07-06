using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Snooker.Platform;

[DependsOn(
    typeof(AbpAccountApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(PlatformApplicationContractsModule),
    typeof(PlatformDomainModule))]
public class PlatformApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<PlatformApplicationModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<PlatformApplicationModule>(validate: true);
        });
    }
}