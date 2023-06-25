using Snooker.Clubs;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.EntityFrameworkCore.Clubs;

public class EfCoreClubRepository : EfCoreRepository<SnookerDbContext, Club, Guid>, IClubRepository
{
    public EfCoreClubRepository(
        IDbContextProvider<SnookerDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}