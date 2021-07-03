using Volo.Abp.Application.Dtos;
using System;

namespace Snooker.Clubs
{
    public class GetClubsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }
    }
}