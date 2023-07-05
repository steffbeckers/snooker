using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Interclub.EntityFrameworkCore;

[ConnectionStringName(InterclubDbProperties.ConnectionStringName)]
public class InterclubDbContext : AbpDbContext<InterclubDbContext>, IInterclubDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public InterclubDbContext(DbContextOptions<InterclubDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureInterclub();
    }
}
