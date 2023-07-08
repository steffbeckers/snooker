using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Platform.EntityFrameworkCore;

[ConnectionStringName(PlatformDbProperties.ConnectionStringName)]
public interface IPlatformDbContext : IEfCoreDbContext
{
}