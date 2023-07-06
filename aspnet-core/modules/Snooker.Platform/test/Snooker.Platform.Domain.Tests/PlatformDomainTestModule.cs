using Snooker.Platform.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Snooker.Platform;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(PlatformEntityFrameworkCoreTestModule)
    )]
public class PlatformDomainTestModule : AbpModule
{

}
