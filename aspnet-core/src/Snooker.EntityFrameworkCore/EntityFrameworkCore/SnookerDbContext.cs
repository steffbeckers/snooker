using Microsoft.EntityFrameworkCore;
using Snooker.ClubPlayers;
using Snooker.Clubs;
using Snooker.Players;
using Snooker.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Snooker.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See SnookerMigrationsDbContext for migrations.
     */

    [ConnectionStringName("Default")]
    public class SnookerDbContext : AbpDbContext<SnookerDbContext>
    {
        public DbSet<ClubPlayer> ClubPlayers { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }
        //public DbSet<AppUser> Users { get; set; }

        // Add DbSet properties for your Aggregate Roots / Entities here. Also map them inside SnookerDbContextModelCreatingExtensions.ConfigureSnooker
        public SnookerDbContext(DbContextOptions<SnookerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            //builder.Entity<AppUser>(b =>
            //{
            //    b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); // Sharing the same table "AbpUsers" with the IdentityUser

            // b.ConfigureByConvention(); b.ConfigureAbpUser();

            //    /* Configure mappings for your additional properties
            //     * Also see the SnookerEfCoreEntityExtensionMappings class
            //     */
            //});

            builder.ConfigureIdentity();

            /* Configure your own tables/entities inside the ConfigureSnooker method */

            builder.ConfigureSnooker();
        }
    }
}