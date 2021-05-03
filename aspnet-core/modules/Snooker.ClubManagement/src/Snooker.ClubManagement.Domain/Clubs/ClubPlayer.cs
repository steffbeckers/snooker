using System;
using Volo.Abp.Domain.Entities;

namespace Snooker.ClubManagement.Clubs
{
    public class ClubPlayer : Entity
    {
        public Guid ClubId { get; private set; }
        public Guid PlayerId { get; private set; }

        protected ClubPlayer()
        {
        }

        public ClubPlayer(Guid clubId, Guid playerId)
        {
            ClubId = clubId;
            PlayerId = playerId;
        }

        public override object[] GetKeys()
        {
            return new object[] { ClubId, PlayerId };
        }
    }
}
