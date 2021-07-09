using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Snooker.Players
{
    public interface IPlayerRepository : IRepository<Player, Guid>
    {
        Task<long> GetCountAsync(
            string filterText = null,
            string firstName = null,
            string lastName = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default);

        Task<IQueryable<Player>> GetFilteredQueryableAsync(
            string filterText = null,
            string firstName = null,
            string lastName = null,
            Guid? userId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0);

        Task<List<Player>> GetListAsync(
            string filterText = null,
            string firstName = null,
            string lastName = null,
            Guid? userId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );
    }
}