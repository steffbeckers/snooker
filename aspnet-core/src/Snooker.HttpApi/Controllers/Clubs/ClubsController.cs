using Microsoft.AspNetCore.Mvc;
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

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ClubDto> UpdateAsync(Guid id, ClubUpdateDto input)
        {
            return _clubsAppService.UpdateAsync(id, input);
        }
    }
}