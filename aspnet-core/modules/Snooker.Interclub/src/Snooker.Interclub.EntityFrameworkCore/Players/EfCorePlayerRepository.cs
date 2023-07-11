using Snooker.Interclub.Players;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Interclub.EntityFrameworkCore.Players;

public class EfCorePlayerRepository : EfCoreRepository<InterclubDbContext, Player, Guid>, IPlayerRepository
{
    public EfCorePlayerRepository(IDbContextProvider<InterclubDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}