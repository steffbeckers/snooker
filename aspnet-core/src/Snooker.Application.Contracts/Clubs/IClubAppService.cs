using Snooker.ClubPlayers;
using Snooker.Players;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Snooker.Clubs
{
    public interface IClubsAppService : IApplicationService
    {
        Task<ClubDto> CreateAsync(ClubCreateDto input);

        Task DeleteAsync(Guid id);

        Task<ClubDto> GetAsync(Guid id);

        Task<PagedResultDto<ClubListDto>> GetListAsync(GetClubsInput input);

        Task<PagedResultDto<ClubPlayerListDto>> GetPlayersListAsync(Guid id, GetClubPlayersInput input);

        Task<ClubDto> UpdateAsync(Guid id, ClubUpdateDto input);
    }
}