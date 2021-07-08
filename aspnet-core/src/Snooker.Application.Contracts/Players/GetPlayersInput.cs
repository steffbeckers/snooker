using Volo.Abp.Application.Dtos;
using System;

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