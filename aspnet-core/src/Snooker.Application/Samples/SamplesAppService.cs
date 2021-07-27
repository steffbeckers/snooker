using Snooker.BackgroundJobs;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
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
    }
}