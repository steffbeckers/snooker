using Localization.Resources.AbpUi;
using Snooker.ClubManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Snooker.ClubManagement
{
    [DependsOn(
        typeof(ClubManagementApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class ClubManagementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(ClubManagementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<ClubManagementResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
