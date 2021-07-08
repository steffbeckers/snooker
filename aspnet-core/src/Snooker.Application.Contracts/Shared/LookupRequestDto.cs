using Volo.Abp.Application.Dtos;

namespace Snooker.Shared
{
    public class LookupRequestDto : PagedResultRequestDto
    {
        public string FilterText { get; set; }

        public LookupRequestDto()
        {
            MaxResultCount = MaxMaxResultCount;
        }
    }
}