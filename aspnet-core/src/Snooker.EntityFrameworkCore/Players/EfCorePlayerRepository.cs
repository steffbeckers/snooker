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

namespace Snooker.Players
{
    public class EfCorePlayerRepository : EfCoreRepository<SnookerDbContext, Player, Guid>, IPlayerRepository
    {
        public EfCorePlayerRepository(IDbContextProvider<SnookerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string firstName = null,
            string lastName = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Player> query = ApplyFilter(
                (await GetDbSetAsync()),
                filterText,
                firstName,
                lastName,
                userId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<IQueryable<Player>> GetFilteredQueryableAsync(
            string filterText = null,
            string firstName = null,
            string lastName = null,
            Guid? userId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0)
        {
            IQueryable<Player> query = ApplyFilter(
                (await GetQueryableAsync()),
                filterText,
                firstName,
                lastName,
                userId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PlayerConsts.GetDefaultSorting(false) : sorting);
            return query.PageBy(skipCount, maxResultCount);
        }

        public async Task<List<Player>> GetListAsync(
            string filterText = null,
            string firstName = null,
            string lastName = null,
            Guid? userId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Player> queryable = await GetFilteredQueryableAsync(
                filterText,
                firstName,
                lastName,
                userId,
                sorting,
                maxResultCount,
                skipCount);
            return await queryable.ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<Player> ApplyFilter(
            IQueryable<Player> query,
            string filterText,
            string firstName = null,
            string lastName = null,
            Guid? userId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.FirstName.Contains(filterText) || e.LastName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.Contains(firstName))
                    .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.Contains(lastName))
                    .WhereIf(userId.HasValue, e => e.UserId == userId);
        }
    }
}