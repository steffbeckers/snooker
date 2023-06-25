using Microsoft.EntityFrameworkCore;
using Snooker.Addresses;
using Snooker.Clubs;
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

    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }

    public DbSet<IdentityRole> Roles { get; set; }

    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }

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
    }
}