using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Snooker.ClubPlayers
{
    public interface IClubPlayerRepository : IRepository<ClubPlayer, Guid>
    {
        Task<long> GetCountAsync(
            string filterText = null,
            Guid? clubId = null,
            Guid? playerId = null,
            bool? isPrimaryClubOfPlayer = null,
            CancellationToken cancellationToken = default);

        Task<IQueryable<ClubPlayer>> GetFilteredQueryableAsync(
            string filterText = null,
            Guid? clubId = null,
            Guid? playerId = null,
            bool? isPrimaryClubOfPlayer = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0);

        Task<List<ClubPlayer>> GetListAsync(
            string filterText = null,
            Guid? clubId = null,
            Guid? playerId = null,
            bool? isPrimaryClubOfPlayer = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);
    }
}