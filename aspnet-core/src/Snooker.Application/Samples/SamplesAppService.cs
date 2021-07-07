using Snooker.BackgroundJobs;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;

namespace Snooker.Samples
{
    [RemoteService(IsEnabled = false)]
    public class SamplesAppService : SnookerAppService, ISamplesAppService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public SamplesAppService(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task QueueManyEmails()
        {
            for (int i = 1; i <= 1000; i++)
            {
                await _backgroundJobManager.EnqueueAsync(new SendEmailJobArgs()
                {
                    Email = "steff@steffbeckers.eu",
                    Subject = "Snooker",
                    Body = $"Test email #{i}"
                });
            }
        }
    }
}