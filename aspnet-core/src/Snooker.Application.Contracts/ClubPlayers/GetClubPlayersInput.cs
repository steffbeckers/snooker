using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.ClubPlayers
{
    public class GetClubPlayersInput : PagedAndSortedResultRequestDto
    {
        public Guid? ClubId { get; set; }
        public string FilterText { get; set; }
        public bool? IsPrimaryClubOfPlayer { get; set; }
        public Guid? PlayerId { get; set; }
    }
}