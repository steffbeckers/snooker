using Microsoft.EntityFrameworkCore;
using Snooker.Interclub.Clubs;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Frames;
using Snooker.Interclub.Players;
using Snooker.Interclub.Seasons;
using Snooker.Interclub.Teams;
using Snooker.Platform.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;

namespace Snooker.Interclub.EntityFrameworkCore;

[ConnectionStringName(InterclubDbProperties.ConnectionStringName)]
public class InterclubDbContext : AbpDbContext<InterclubDbContext>, IInterclubDbContext
{
    public InterclubDbContext(DbContextOptions<InterclubDbContext> options)
        : base(options)
    {
    }

    public DbSet<IdentityClaimType> ClaimTypes { get; set; }

    public DbSet<Club> Clubs { get; set; }

    public DbSet<Division> Divisions { get; set; }

    public DbSet<Frame> Frames { get; set; }

    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }

    public DbSet<Player> Players { get; set; }

    public DbSet<IdentityRole> Roles { get; set; }

    public DbSet<Season> Seasons { get; set; }

    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }

    public DbSet<TeamPlayer> TeamPlayers { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    public DbSet<IdentityUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePlatform();
        builder.ConfigureInterclub();
    }
}