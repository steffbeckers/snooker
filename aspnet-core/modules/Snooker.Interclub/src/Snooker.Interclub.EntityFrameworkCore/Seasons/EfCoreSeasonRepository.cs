using Snooker.Interclub.Seasons;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Interclub.EntityFrameworkCore.Seasons;

public class EfCoreSeasonRepository : EfCoreRepository<InterclubDbContext, Season, Guid>, ISeasonRepository
{
    public EfCoreSeasonRepository(IDbContextProvider<InterclubDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}