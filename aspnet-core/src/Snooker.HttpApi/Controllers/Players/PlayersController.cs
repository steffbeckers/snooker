using Microsoft.AspNetCore.Mvc;
using Snooker.ClubPlayers;
using Snooker.Clubs;
using Snooker.Players;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Snooker.Controllers.Players
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Player")]
    [Route("api/app/players")]
    public class PlayersController : SnookerController, IPlayersAppService
    {
        private readonly IPlayersAppService _playersAppService;

        public PlayersController(IPlayersAppService playersAppService)
        {
            _playersAppService = playersAppService;
        }

        [HttpPost]
        public virtual Task<PlayerDto> CreateAsync(PlayerCreateDto input)
        {
            return _playersAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _playersAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PlayerDto> GetAsync(Guid id)
        {
            return _playersAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("{id}/club")]
        public virtual Task<ClubDto> GetClubAsync(Guid id)
        {
            return _playersAppService.GetClubAsync(id);
        }

        [HttpGet]
        [Route("{id}/clubs")]
        public virtual Task<PagedResultDto<ClubPlayerListDto>> GetClubsListAsync(Guid id, GetClubPlayersInput input)
        {
            return _playersAppService.GetClubsListAsync(id, input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<PlayerListDto>> GetListAsync(GetPlayersInput input)
        {
            return _playersAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}/profile")]
        public virtual Task<PlayerProfileDto> GetProfileAsync(Guid id)
        {
            return _playersAppService.GetProfileAsync(id);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PlayerDto> UpdateAsync(Guid id, PlayerUpdateDto input)
        {
            return _playersAppService.UpdateAsync(id, input);
        }
    }
}