using Microsoft.EntityFrameworkCore;
using Snooker.ClubManagement.Clubs;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.ClubManagement.EntityFrameworkCore
{
    [ConnectionStringName(ClubManagementDbProperties.ConnectionStringName)]
    public interface IClubManagementDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Club> Clubs { get; }
    }
}