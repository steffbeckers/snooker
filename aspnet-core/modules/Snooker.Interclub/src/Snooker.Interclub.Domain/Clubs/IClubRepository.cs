using System;
using Volo.Abp.Domain.Repositories;

namespace Snooker.Interclub.Clubs;

public interface IClubRepository : IRepository<Club, Guid>
{
}