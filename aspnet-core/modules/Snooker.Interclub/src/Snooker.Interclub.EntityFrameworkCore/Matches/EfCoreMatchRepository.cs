using Snooker.Interclub.Matches;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Interclub.EntityFrameworkCore.Matches;

public class EfCoreMatchRepository : EfCoreRepository<InterclubDbContext, Match, Guid>, IMatchRepository
{
    public EfCoreMatchRepository(IDbContextProvider<InterclubDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}