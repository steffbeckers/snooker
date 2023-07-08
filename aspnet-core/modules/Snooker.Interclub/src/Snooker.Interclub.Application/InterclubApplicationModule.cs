using Microsoft.Extensions.DependencyInjection;
using Snooker.Platform;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Snooker.Interclub;

[DependsOn(
    typeof(InterclubApplicationContractsModule),
    typeof(InterclubDomainModule),
    typeof(PlatformApplicationModule))]
public class InterclubApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<InterclubApplicationModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<InterclubApplicationModule>(validate: true);
        });
    }
}