using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Snooker.ClubPlayers
{
    public class ClubPlayer : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid ClubId { get; set; }
        public virtual bool IsPrimaryClubOfPlayer { get; set; }
        public virtual Guid PlayerId { get; set; }

        public ClubPlayer(Guid id, Guid clubId, Guid playerId)
        {
            Id = id;
            ClubId = clubId;
            PlayerId = playerId;
        }

        private ClubPlayer()
        {
        }
    }
}