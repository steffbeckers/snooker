using Snooker.Interclub.Clubs;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Interclub.EntityFrameworkCore.Clubs;

public class EfCoreClubRepository : EfCoreRepository<InterclubDbContext, Club, Guid>, IClubRepository
{
    public EfCoreClubRepository(IDbContextProvider<InterclubDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}