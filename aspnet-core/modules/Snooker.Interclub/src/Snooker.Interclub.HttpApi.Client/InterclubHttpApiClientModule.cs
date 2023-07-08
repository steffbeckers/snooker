using Microsoft.Extensions.DependencyInjection;
using Snooker.Platform;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Snooker.Interclub;

[DependsOn(
    typeof(InterclubApplicationContractsModule),
    typeof(PlatformHttpApiClientModule))]
public class InterclubHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(InterclubApplicationContractsModule).Assembly,
            InterclubRemoteServiceConsts.RemoteServiceName);

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<InterclubHttpApiClientModule>();
        });
    }
}