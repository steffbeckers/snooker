using System;
using Volo.Abp.Domain.Repositories;

namespace Snooker.Interclub.Players;

public interface IPlayerRepository : IRepository<Player, Guid>
{
}