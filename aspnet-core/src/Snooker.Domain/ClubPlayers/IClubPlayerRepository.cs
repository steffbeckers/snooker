using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Snooker.ClubPlayers
{
    public interface IClubPlayerRepository : IRepository<ClubPlayer, Guid>
    {
        Task<List<ClubPlayer>> GetListAsync(
            string filterText = null,
            Guid? clubId = null,
            Guid? playerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            Guid? clubId = null,
            Guid? playerId = null,
            CancellationToken cancellationToken = default);
    }
}