using System;
using Volo.Abp.Domain.Repositories;

namespace Snooker.Interclub.Matches;

public interface IMatchRepository : IRepository<Match, Guid>
{
}