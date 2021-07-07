using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Snooker.Samples
{
    public interface ISamplesAppService : IApplicationService
    {
        Task QueueManyEmails();
    }
}