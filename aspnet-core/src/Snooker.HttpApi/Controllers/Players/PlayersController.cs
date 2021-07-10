using Microsoft.AspNetCore.Mvc;
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
        public virtual Task<PagedResultDto<PlayerListDto>> GetListAsync(GetPlayersInput input)
        {
            return _playersAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PlayerDto> UpdateAsync(Guid id, PlayerUpdateDto input)
        {
            return _playersAppService.UpdateAsync(id, input);
        }
    }
}