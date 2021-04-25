using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Snooker.ClubManagement.Clubs
{
    public interface IClubRepository : IRepository<Club, Guid>
    {
        Task<Club> FindByNameAsync(string name);
    }
}
