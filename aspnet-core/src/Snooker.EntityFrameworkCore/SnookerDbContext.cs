using Microsoft.EntityFrameworkCore;
using Snooker.Platform.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class SnookerDbContext : AbpDbContext<SnookerDbContext>
{
    public SnookerDbContext(DbContextOptions<SnookerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePlatform();
        //builder.ConfigureInterclub();
    }
}