using System;
using Volo.Abp.Domain.Repositories;

namespace Snooker.Interclub.Divisions;

public interface IDivisionRepository : IRepository<Division, Guid>
{
}