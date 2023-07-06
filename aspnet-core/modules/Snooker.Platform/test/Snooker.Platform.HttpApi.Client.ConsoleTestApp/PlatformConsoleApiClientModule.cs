using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Snooker.Platform;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(PlatformHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class PlatformConsoleApiClientModule : AbpModule
{

}
