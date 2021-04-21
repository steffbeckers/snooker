using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Snooker.ClubManagement
{
    [DependsOn(
        typeof(ClubManagementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class ClubManagementHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "ClubManagement";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(ClubManagementApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
