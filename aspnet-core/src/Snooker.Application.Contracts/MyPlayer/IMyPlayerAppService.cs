using Snooker.Players;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Snooker.MyPlayer
{
    public interface IMyPlayerAppService : IApplicationService
    {
        Task<PlayerDto> GetAsync();
    }
}