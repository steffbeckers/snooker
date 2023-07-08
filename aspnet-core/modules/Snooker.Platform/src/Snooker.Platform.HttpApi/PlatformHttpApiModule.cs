using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Snooker.Platform.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Snooker.Platform;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(PlatformApplicationContractsModule))]
public class PlatformHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<PlatformResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(PlatformHttpApiModule).Assembly);
        });
    }
}