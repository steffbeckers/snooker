using Microsoft.AspNetCore.Authorization;
using Snooker.Permissions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace Snooker.Players
{
    [RemoteService(IsEnabled = false)]
    [Authorize(SnookerPermissions.Players.Default)]
    public class PlayersAppService : ApplicationService, IPlayersAppService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersAppService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [Authorize(SnookerPermissions.Players.Create)]
        public virtual async Task<PlayerDto> CreateAsync(PlayerCreateDto input)
        {
            Player player = new Player(GuidGenerator.Create(), input.FirstName, input.LastName);
            player = await _playerRepository.InsertAsync(player, autoSave: true);

            return ObjectMapper.Map<Player, PlayerDto>(player);
        }

        [Authorize(SnookerPermissions.Players.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _playerRepository.DeleteAsync(id);
        }

        public virtual async Task<PlayerDto> GetAsync(Guid id)
        {
            Player player = await _playerRepository.GetAsync(id);

            return ObjectMapper.Map<Player, PlayerDto>(player);
        }

        public virtual async Task<PagedResultDto<PlayerListDto>> GetListAsync(GetPlayersInput input)
        {
            long totalCount = await _playerRepository.GetCountAsync(input.FilterText, input.FirstName, input.LastName, input.UserId);
            IQueryable<Player> playerQueryable = await _playerRepository.GetFilteredQueryableAsync(
                input.FilterText,
                input.FirstName,
                input.LastName,
                input.UserId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);
            IQueryable<PlayerListDto> playerListDtoQueryable = ObjectMapper.GetMapper().ProjectTo<PlayerListDto>(playerQueryable);

            return new PagedResultDto<PlayerListDto>
            {
                TotalCount = totalCount,
                Items = await AsyncExecuter.ToListAsync(playerListDtoQueryable)
            };
        }

        [Authorize(SnookerPermissions.Players.Edit)]
        public virtual async Task<PlayerDto> UpdateAsync(Guid id, PlayerUpdateDto input)
        {
            Player player = await _playerRepository.GetAsync(id);
            player.FirstName = input.FirstName;
            player.LastName = input.LastName;

            // TODO: Assign user through player manager?
            player.UserId = input.UserId;

            player = await _playerRepository.UpdateAsync(player, autoSave: true);

            return ObjectMapper.Map<Player, PlayerDto>(player);
        }
    }
}