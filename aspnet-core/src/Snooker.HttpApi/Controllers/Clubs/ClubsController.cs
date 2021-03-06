using Microsoft.AspNetCore.Mvc;
using Snooker.ClubPlayers;
using Snooker.Clubs;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Snooker.Controllers.Clubs
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Club")]
    [Route("api/app/clubs")]
    public class ClubsController : SnookerController, IClubsAppService
    {
        private readonly IClubsAppService _clubsAppService;

        public ClubsController(IClubsAppService clubsAppService)
        {
            _clubsAppService = clubsAppService;
        }

        [HttpPost]
        [Route("{id}/players")]
        public virtual Task<ClubPlayerDto> AddPlayerAsync(Guid id, Guid playerId)
        {
            return _clubsAppService.AddPlayerAsync(id, playerId);
        }

        [HttpPost]
        public virtual Task<ClubDto> CreateAsync(ClubCreateDto input)
        {
            return _clubsAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _clubsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ClubDto> GetAsync(Guid id)
        {
            return _clubsAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ClubListDto>> GetListAsync(GetClubsInput input)
        {
            return _clubsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}/players")]
        public virtual Task<PagedResultDto<ClubPlayerListDto>> GetPlayersListAsync(Guid id, GetClubPlayersInput input)
        {
            return _clubsAppService.GetPlayersListAsync(id, input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ClubDto> UpdateAsync(Guid id, ClubUpdateDto input)
        {
            return _clubsAppService.UpdateAsync(id, input);
        }
    }
}