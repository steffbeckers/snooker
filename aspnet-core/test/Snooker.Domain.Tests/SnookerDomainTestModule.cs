using Snooker.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Snooker;

[DependsOn(
    typeof(SnookerEntityFrameworkCoreTestModule)
    )]
public class SnookerDomainTestModule : AbpModule
{

}
