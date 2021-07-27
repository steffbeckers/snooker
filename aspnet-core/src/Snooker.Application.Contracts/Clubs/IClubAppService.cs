using Snooker.ClubPlayers;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Snooker.Clubs
{
    public interface IClubsAppService : IApplicationService
    {
        Task<ClubPlayerDto> AddPlayerAsync(Guid id, Guid playerId);

        Task<ClubDto> CreateAsync(ClubCreateDto input);

        Task DeleteAsync(Guid id);

        Task<ClubDto> GetAsync(Guid id);

        Task<PagedResultDto<ClubListDto>> GetListAsync(GetClubsInput input);

        Task<PagedResultDto<ClubPlayerListDto>> GetPlayersListAsync(Guid id, GetClubPlayersInput input);

        Task<ClubDto> UpdateAsync(Guid id, ClubUpdateDto input);
    }
}