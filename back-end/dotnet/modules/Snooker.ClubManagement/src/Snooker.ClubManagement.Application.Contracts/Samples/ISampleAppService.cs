using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Snooker.ClubManagement.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}
