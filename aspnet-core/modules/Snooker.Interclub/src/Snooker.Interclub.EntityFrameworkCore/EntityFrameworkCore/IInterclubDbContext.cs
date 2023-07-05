using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Interclub.EntityFrameworkCore;

[ConnectionStringName(InterclubDbProperties.ConnectionStringName)]
public interface IInterclubDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
