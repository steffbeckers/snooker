using Microsoft.AspNetCore.Mvc;
using Snooker.Samples;
using System.Threading.Tasks;
using Volo.Abp;

namespace Snooker.Controllers.Samples
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Sample")]
    [Route("api/app/samples")]
    public class SamplesController : SnookerController, ISamplesAppService
    {
        private readonly ISamplesAppService _samplesAppService;

        public SamplesController(ISamplesAppService samplesAppService)
        {
            _samplesAppService = samplesAppService;
        }

        [HttpGet]
        [Route("queue-many-emails")]
        public virtual Task QueueManyEmails()
        {
            return _samplesAppService.QueueManyEmails();
        }

        [HttpGet]
        [Route("queue-many-emails-with-default-email-sender")]
        public virtual Task QueueManyEmailsWithDefaultEmailSender()
        {
            return _samplesAppService.QueueManyEmailsWithDefaultEmailSender();
        }

        [HttpGet]
        [Route("throw-error-from-domain-entity")]
        public virtual Task ThrowErrorFromDomainEntity()
        {
            return _samplesAppService.ThrowErrorFromDomainEntity();
        }
    }
}