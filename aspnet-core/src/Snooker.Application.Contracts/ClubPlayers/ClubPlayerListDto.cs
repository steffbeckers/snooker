using Snooker.Clubs;
using Snooker.Players;
using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.ClubPlayers
{
    public class ClubPlayerListDto : EntityDto<Guid>
    {
        public ClubListDto Club { get; set; }
        public bool IsPrimaryClubOfPlayer { get; set; }
        public PlayerListDto Player { get; set; }
    }
}