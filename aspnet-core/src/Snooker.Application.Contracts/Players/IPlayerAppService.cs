using Snooker.ClubPlayers;
using Snooker.Clubs;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Snooker.Players
{
    public interface IPlayersAppService : IApplicationService
    {
        Task<PlayerDto> CreateAsync(PlayerCreateDto input);

        Task DeleteAsync(Guid id);

        Task<PlayerDto> GetAsync(Guid id);

        Task<ClubDto> GetClubAsync(Guid id);

        Task<PagedResultDto<ClubPlayerListDto>> GetClubsListAsync(Guid id, GetClubPlayersInput getClubPlayersInput);

        Task<PagedResultDto<PlayerListDto>> GetListAsync(GetPlayersInput input);

        Task<PlayerProfileDto> GetProfileAsync(Guid id);

        Task<PlayerDto> UpdateAsync(Guid id, PlayerUpdateDto input);
    }
}