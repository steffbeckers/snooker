using Volo.Abp.Application.Dtos;
using System;

namespace Snooker.ClubPlayers
{
    public class GetClubPlayersInput : PagedAndSortedResultRequestDto
    {
        public Guid? ClubId { get; set; }
        public string FilterText { get; set; }
        public Guid? PlayerId { get; set; }
    }
}