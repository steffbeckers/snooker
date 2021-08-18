using Microsoft.AspNetCore.Http;

namespace Snooker.Samples
{
    public class UploadFile3Dto
    {
        public IFormFile File { get; set; }

        public string FirstName { get; set; }

        public string Test { get; set; }
    }
}