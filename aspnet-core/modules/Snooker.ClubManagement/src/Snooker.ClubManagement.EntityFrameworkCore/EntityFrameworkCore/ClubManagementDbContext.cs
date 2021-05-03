using Microsoft.EntityFrameworkCore;
using Snooker.ClubManagement.Clubs;
using Snooker.ClubManagement.Players;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.ClubManagement.EntityFrameworkCore
{
    [ConnectionStringName(ClubManagementDbProperties.ConnectionStringName)]
    public class ClubManagementDbContext : AbpDbContext<ClubManagementDbContext>, IClubManagementDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }

        public ClubManagementDbContext(DbContextOptions<ClubManagementDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureClubManagement();
        }
    }
}