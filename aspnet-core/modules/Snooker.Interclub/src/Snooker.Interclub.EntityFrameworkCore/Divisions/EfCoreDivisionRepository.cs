using Snooker.Interclub.Divisions;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Interclub.EntityFrameworkCore.Divisions;

public class EfCoreDivisionRepository : EfCoreRepository<InterclubDbContext, Division, Guid>, IDivisionRepository
{
    public EfCoreDivisionRepository(IDbContextProvider<InterclubDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}