using Microsoft.Extensions.DependencyInjection;
using Snooker.Interclub;
using Snooker.Platform;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Snooker;

[DependsOn(
    typeof(InterclubHttpApiClientModule),
    typeof(PlatformHttpApiClientModule),
    typeof(SnookerApplicationContractsModule))]
public class SnookerHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(SnookerApplicationContractsModule).Assembly,
            RemoteServiceName);

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SnookerHttpApiClientModule>();
        });
    }
}