using Microsoft.AspNetCore.Mvc;
using Snooker.MyPlayer;
using Snooker.Players;
using System.Threading.Tasks;
using Volo.Abp;

namespace Snooker.Controllers.MyPlayer
{
    [RemoteService]
    [Area("app")]
    [ControllerName("MyPlayer")]
    [Route("api/app/my-player")]
    public class MyPlayerController : SnookerController, IMyPlayerAppService
    {
        private readonly IMyPlayerAppService _myPlayerAppService;

        public MyPlayerController(IMyPlayerAppService myPlayerAppService)
        {
            _myPlayerAppService = myPlayerAppService;
        }

        [HttpGet]
        public virtual Task<PlayerDto> GetAsync()
        {
            return _myPlayerAppService.GetAsync();
        }
    }
}