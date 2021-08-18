using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Snooker.Samples
{
    public interface ISamplesAppService : IApplicationService
    {
        Task QueueManyEmails();

        Task QueueManyEmailsWithDefaultEmailSender();

        Task ThrowErrorFromDomainEntity();

        Task UploadFile(IFormFile file);

        Task UploadFile2(string firstName, string test, IFormFile file);

        Task UploadFile3(UploadFile3Dto input);

        Task UploadFiles(UploadFilesDto input);
    }
}