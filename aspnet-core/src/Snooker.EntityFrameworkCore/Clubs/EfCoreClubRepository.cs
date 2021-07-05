using Microsoft.EntityFrameworkCore;
using Snooker.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Clubs
{
    public class EfCoreClubRepository : EfCoreRepository<SnookerDbContext, Club, Guid>, IClubRepository
    {
        public EfCoreClubRepository(IDbContextProvider<SnookerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Club> query = ApplyFilter((await GetDbSetAsync()), filterText, name);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Club>> GetListAsync(
            string filterText = null,
            string name = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Club> query = ApplyFilter((await GetQueryableAsync()), filterText, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ClubConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<Club> ApplyFilter(
            IQueryable<Club> query,
            string filterText,
            string name = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name));
        }
    }
}