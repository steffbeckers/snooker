using System;
using Volo.Abp.Domain.Repositories;

namespace Snooker.Interclub.Teams;

public interface ITeamRepository : IRepository<Team, Guid>
{
}