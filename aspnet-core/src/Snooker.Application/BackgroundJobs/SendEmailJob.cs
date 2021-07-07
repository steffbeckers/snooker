using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;

namespace Snooker.BackgroundJobs
{
    public class SendEmailJob : AsyncBackgroundJob<SendEmailJobArgs>, ITransientDependency
    {
        private readonly IEmailSender _emailSender;

        public SendEmailJob(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public override async Task ExecuteAsync(SendEmailJobArgs args)
        {
            await _emailSender.SendAsync(args.Email, args.Subject, args.Body);
        }
    }
}