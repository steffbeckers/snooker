using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Snooker.Interclub;

[DependsOn(
    typeof(InterclubApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class InterclubHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(InterclubApplicationContractsModule).Assembly,
            InterclubRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<InterclubHttpApiClientModule>();
        });

    }
}
