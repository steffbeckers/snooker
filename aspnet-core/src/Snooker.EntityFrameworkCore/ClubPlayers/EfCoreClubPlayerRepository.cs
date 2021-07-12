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

namespace Snooker.ClubPlayers
{
    public class EfCoreClubPlayerRepository : EfCoreRepository<SnookerDbContext, ClubPlayer, Guid>, IClubPlayerRepository
    {
        public EfCoreClubPlayerRepository(IDbContextProvider<SnookerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            Guid? clubId = null,
            Guid? playerId = null,
            bool? isPrimaryClubOfPlayer = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<ClubPlayer> query = ApplyFilter(
                (await GetDbSetAsync()),
                filterText,
                clubId,
                playerId,
                isPrimaryClubOfPlayer);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<IQueryable<ClubPlayer>> GetFilteredQueryableAsync(
            string filterText = null,
            Guid? clubId = null,
            Guid? playerId = null,
            bool? isPrimaryClubOfPlayer = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0)
        {
            IQueryable<ClubPlayer> query = ApplyFilter(
                (await GetQueryableAsync()),
                filterText,
                clubId,
                playerId,
                isPrimaryClubOfPlayer);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ClubPlayerConsts.GetDefaultSorting(false) : sorting);
            return query.PageBy(skipCount, maxResultCount);
        }

        public async Task<List<ClubPlayer>> GetListAsync(
            string filterText = null,
            Guid? clubId = null,
            Guid? playerId = null,
            bool? isPrimaryClubOfPlayer = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            IQueryable<ClubPlayer> queryable = await GetFilteredQueryableAsync(
                filterText,
                clubId,
                playerId,
                isPrimaryClubOfPlayer,
                sorting,
                maxResultCount,
                skipCount);
            return await queryable.ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<ClubPlayer> ApplyFilter(
            IQueryable<ClubPlayer> query,
            string filterText,
            Guid? clubId = null,
            Guid? playerId = null,
            bool? isPrimaryClubOfPlayer = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(clubId.HasValue, e => e.ClubId == clubId)
                    .WhereIf(playerId.HasValue, e => e.PlayerId == playerId)
                    .WhereIf(isPrimaryClubOfPlayer.HasValue, e => e.IsPrimaryClubOfPlayer == isPrimaryClubOfPlayer);
        }
    }
}