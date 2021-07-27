using Microsoft.AspNetCore.Authorization;
using Snooker.Players;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Snooker.MyPlayer
{
    [RemoteService(IsEnabled = false)]
    [Authorize]
    public class MyPlayerAppService : SnookerAppService, IMyPlayerAppService
    {
        private readonly IPlayerRepository _playerRepository;

        public MyPlayerAppService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public virtual async Task<PlayerDto> GetAsync()
        {
            Player player = await _playerRepository.GetAsync(x => x.UserId == CurrentUser.Id);

            return ObjectMapper.Map<Player, PlayerDto>(player);
        }
    }
}