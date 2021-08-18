using Microsoft.AspNetCore.Http;
using Snooker.BackgroundJobs;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;

namespace Snooker.Samples
{
    [RemoteService(IsEnabled = false)]
    public class SamplesAppService : SnookerAppService, ISamplesAppService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IEmailSender _emailSender;

        public SamplesAppService(
            IBackgroundJobManager backgroundJobManager,
            IEmailSender emailSender)
        {
            _backgroundJobManager = backgroundJobManager;
            _emailSender = emailSender;
        }

        public async Task QueueManyEmails()
        {
            for (int i = 1; i <= 10; i++)
            {
                await _backgroundJobManager.EnqueueAsync(new SendEmailJobArgs()
                {
                    Email = "steff@steffbeckers.eu",
                    Subject = "Snooker",
                    Body = $"Test email #{i}"
                });
            }
        }

        public async Task QueueManyEmailsWithDefaultEmailSender()
        {
            for (int i = 1; i <= 10; i++)
            {
                await _emailSender.QueueAsync(
                    "steff@steffbeckers.eu",
                    "Snooker",
                    $"Test email #{i}");
            }
        }

        public Task ThrowErrorFromDomainEntity()
        {
            EntityWithErrorMethod entityWithErrorMethod = new EntityWithErrorMethod();
            entityWithErrorMethod.ThrowBusinessException();

            return Task.CompletedTask;
        }

        public Task UploadFile(IFormFile file)
        {
            throw new System.NotImplementedException();
        }

        public Task UploadFile2(string firstName, string test, IFormFile file)
        {
            throw new System.NotImplementedException();
        }

        public Task UploadFile3(UploadFile3Dto input)
        {
            throw new System.NotImplementedException();
        }

        public Task UploadFiles(UploadFilesDto input)
        {
            throw new System.NotImplementedException();
        }
    }
}