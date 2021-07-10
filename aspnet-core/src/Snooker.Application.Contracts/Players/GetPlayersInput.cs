using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.Players
{
    public class GetPlayersInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? UserId { get; set; }
    }
}