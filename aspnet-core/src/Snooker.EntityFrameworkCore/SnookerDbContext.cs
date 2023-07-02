using Microsoft.EntityFrameworkCore;
using Snooker.Addresses;
using Snooker.Clubs;
using Snooker.Divisions;
using Snooker.Frames;
using Snooker.Leagues;
using Snooker.Matches;
using Snooker.Players;
using Snooker.Seasons;
using Snooker.TeamPlayers;
using Snooker.Teams;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Snooker.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class SnookerDbContext :
    AbpDbContext<SnookerDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    public SnookerDbContext(DbContextOptions<SnookerDbContext> options)
        : base(options)
    {
    }

    public DbSet<IdentityClaimType> ClaimTypes { get; set; }

    public DbSet<Club> Clubs { get; set; }

    public DbSet<Division> Divisions { get; set; }

    public DbSet<Frame> Frames { get; set; }

    public DbSet<League> Leagues { get; set; }

    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    public DbSet<Match> Matches { get; set; }

    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }

    public DbSet<Player> Players { get; set; }

    public DbSet<IdentityRole> Roles { get; set; }

    public DbSet<Season> Seasons { get; set; }

    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }

    public DbSet<TeamPlayer> TeamPlayers { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<IdentityUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        builder.Entity<Club>(b =>
        {
            b.ToTable(SnookerConsts.DbTablePrefix + "Clubs", SnookerConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(ClubConsts.NameMaxLength);
            b.Property(x => x.Email).HasMaxLength(ClubConsts.EmailMaxLength);
            b.Property(x => x.PhoneNumber).HasMaxLength(ClubConsts.PhoneNumberMaxLength);
            b.Property(x => x.Website).HasMaxLength(ClubConsts.WebsiteMaxLength);
            b.OwnsOne(
                x => x.Address,
                b =>
                {
                    b.ToTable(SnookerConsts.DbTablePrefix + "ClubAddresses", SnookerConsts.DbSchema);
                    b.Property(x => x.Street).HasMaxLength(AddressConsts.StreetMaxLength);
                    b.Property(x => x.Number).HasMaxLength(AddressConsts.NumberMaxLength);
                    b.Property(x => x.PostalCode).HasMaxLength(AddressConsts.PostalCodeMaxLength);
                    b.Property(x => x.City).HasMaxLength(AddressConsts.CityMaxLength);
                });
        });

        builder.Entity<Division>(b =>
        {
            b.ToTable(SnookerConsts.DbTablePrefix + "Divisions", SnookerConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(DivisionConsts.NameMaxLength);
        });

        builder.Entity<Frame>(b =>
        {
            b.ToTable(SnookerConsts.DbTablePrefix + "Frames", SnookerConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Match).WithMany(x => x.Frames).HasForeignKey(x => x.MatchId);
            b.HasOne(x => x.HomePlayer).WithMany().HasForeignKey(x => x.HomePlayerId);
            b.HasOne(x => x.AwayPlayer).WithMany().HasForeignKey(x => x.AwayPlayerId);
        });

        builder.Entity<League>(b =>
        {
            b.ToTable(SnookerConsts.DbTablePrefix + "Leagues", SnookerConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(LeagueConsts.NameMaxLength);
        });

        builder.Entity<Match>(b =>
        {
            b.ToTable(SnookerConsts.DbTablePrefix + "Matches", SnookerConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.AwayTeam).WithMany().HasForeignKey(x => x.AwayTeamId);
            b.HasOne(x => x.HomeTeam).WithMany().HasForeignKey(x => x.HomeTeamId);
        });

        builder.Entity<Player>(b =>
        {
            b.ToTable(SnookerConsts.DbTablePrefix + "Players", SnookerConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.FirstName).IsRequired().HasMaxLength(PlayerConsts.FirstNameMaxLength);
            b.Property(x => x.LastName).IsRequired().HasMaxLength(PlayerConsts.LastNameMaxLength);
            b.HasOne(x => x.Club).WithMany(x => x.Players).HasForeignKey(x => x.ClubId);
            b.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        });

        builder.Entity<Season>(b =>
        {
            b.ToTable(SnookerConsts.DbTablePrefix + "Seasons", SnookerConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.StartDate).IsRequired();
            b.Property(x => x.EndDate).IsRequired();
        });

        builder.Entity<Team>(b =>
        {
            b.ToTable(SnookerConsts.DbTablePrefix + "Teams", SnookerConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(ClubConsts.NameMaxLength);
            b.HasOne(x => x.Club).WithMany(x => x.Teams).HasForeignKey(x => x.ClubId);
            b.HasOne(x => x.Division).WithMany(x => x.Teams).HasForeignKey(x => x.DivisionId);
        });

        builder.Entity<TeamPlayer>(b =>
        {
            b.ToTable(SnookerConsts.DbTablePrefix + "TeamPlayers", SnookerConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Team).WithMany(x => x.Players).HasForeignKey(x => x.TeamId);
            b.HasOne(x => x.Player).WithMany(x => x.Teams).HasForeignKey(x => x.PlayerId);
        });
    }
}