using Volo.Abp.Application.Dtos;

namespace Snooker.Clubs
{
    public class GetClubsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
        public string Name { get; set; }
    }
}