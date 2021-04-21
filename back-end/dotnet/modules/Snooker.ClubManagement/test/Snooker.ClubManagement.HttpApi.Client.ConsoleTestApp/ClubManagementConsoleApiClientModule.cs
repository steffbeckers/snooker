using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Snooker.ClubManagement
{
    [DependsOn(
        typeof(ClubManagementHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class ClubManagementConsoleApiClientModule : AbpModule
    {
        
    }
}
