using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Snooker.Interclub;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(InterclubHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class InterclubConsoleApiClientModule : AbpModule
{

}
