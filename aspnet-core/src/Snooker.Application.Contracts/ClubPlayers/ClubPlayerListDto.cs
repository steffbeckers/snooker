using Snooker.Clubs;
using Snooker.Players;
using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.ClubPlayers
{
    public class ClubPlayerListDto : EntityDto<Guid>
    {
        public ClubListDto Club { get; set; }
        public Guid ClubId { get; set; }
        public PlayerListDto Player { get; set; }
        public Guid PlayerId { get; set; }
    }
}