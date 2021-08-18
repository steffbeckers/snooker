using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        [Route("upload-file")]
        public virtual Task UploadFile(IFormFile file)
        {
            return _samplesAppService.UploadFile(file);
        }

        [HttpPost]
        [Route("upload-file-2")]
        public virtual Task UploadFile2([FromForm] string firstName, [FromForm] string test, IFormFile file)
        {
            return _samplesAppService.UploadFile2(firstName, test, file);
        }

        [HttpPost]
        [Route("upload-file-3")]
        public virtual Task UploadFile3([FromForm] UploadFile3Dto input)
        {
            return _samplesAppService.UploadFile3(input);
        }

        [HttpPost]
        [Route("upload-files")]
        public virtual Task UploadFiles([FromForm] UploadFilesDto input)
        {
            return _samplesAppService.UploadFiles(input);
        }
    }
}