using System;
using Volo.Abp.Domain.Repositories;

namespace Snooker.Interclub.Seasons;

public interface ISeasonRepository : IRepository<Season, Guid>
{
}