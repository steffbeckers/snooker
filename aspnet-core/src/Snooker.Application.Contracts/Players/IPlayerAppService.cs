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

        Task<PagedResultDto<PlayerListDto>> GetListAsync(GetPlayersInput input);

        Task<PlayerDto> UpdateAsync(Guid id, PlayerUpdateDto input);
    }
}