using Localization.Resources.AbpUi;
using Snooker.Interclub.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Snooker.Interclub;

[DependsOn(
    typeof(InterclubApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class InterclubHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(InterclubHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<InterclubResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
