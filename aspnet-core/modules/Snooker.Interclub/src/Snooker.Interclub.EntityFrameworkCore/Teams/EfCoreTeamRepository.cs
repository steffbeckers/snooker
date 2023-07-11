using Snooker.Interclub.Teams;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Interclub.EntityFrameworkCore.Teams;

public class EfCoreTeamRepository : EfCoreRepository<InterclubDbContext, Team, Guid>, ITeamRepository
{
    public EfCoreTeamRepository(IDbContextProvider<InterclubDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}