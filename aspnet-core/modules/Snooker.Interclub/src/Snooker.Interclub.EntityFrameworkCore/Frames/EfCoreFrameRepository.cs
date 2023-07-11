using Snooker.Interclub.Frames;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Interclub.EntityFrameworkCore.Frames;

public class EfCoreFrameRepository : EfCoreRepository<InterclubDbContext, Frame, Guid>, IFrameRepository
{
    public EfCoreFrameRepository(IDbContextProvider<InterclubDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}