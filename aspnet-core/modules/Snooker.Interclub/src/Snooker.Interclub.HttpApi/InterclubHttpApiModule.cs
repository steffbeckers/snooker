using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Snooker.Interclub.Localization;
using Snooker.Platform;
using Volo.Abp.Localization;

using Volo.Abp.Modularity;

namespace Snooker.Interclub;

[DependsOn(
    typeof(InterclubApplicationContractsModule),
    typeof(PlatformHttpApiModule))]
public class InterclubHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<InterclubResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(InterclubHttpApiModule).Assembly);
        });
    }
}