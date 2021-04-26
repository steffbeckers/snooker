using Microsoft.EntityFrameworkCore;
using Snooker.ClubManagement.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.ClubManagement.Clubs
{
    public class EfCoreClubRepository
        : EfCoreRepository<ClubManagementDbContext, Club, Guid>,
            IClubRepository
    {
        public EfCoreClubRepository(
            IDbContextProvider<ClubManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Club> FindByNameAsync(string name)
        {
            DbSet<Club> dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
