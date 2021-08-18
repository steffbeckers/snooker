using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Snooker.Samples
{
    public class UploadFilesDto
    {
        public ICollection<IFormFile> Files { get; set; }

        public IFormFile Test { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}