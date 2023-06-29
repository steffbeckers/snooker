using Snooker.Teams;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.EntityFrameworkCore.Teams;

public class EfCoreTeamRepository : EfCoreRepository<SnookerDbContext, Team, Guid>, ITeamRepository
{
    public EfCoreTeamRepository(
        IDbContextProvider<SnookerDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}