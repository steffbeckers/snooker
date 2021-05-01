using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Snooker.ClubManagement.Clubs
{
    public class Club : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public ICollection<ClubPlayer> Players { get; protected set; }

        public Club()
        {
            // This constructor is for deserialization / ORM purpose
        }

        public Club(
            Guid id,
            [NotNull] string name
        ) : base(id)
        {
            SetName(name);
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: ClubConsts.NameMaxLength
            );
        }

        public void AddPlayer(Guid playerId)
        {
            if (Players.Any(x => x.PlayerId == playerId))
            {
                return;
            }

            Players.Add(new ClubPlayer(Id, playerId));
        }

        public void RemovePlayer(Guid playerId)
        {
            Players.RemoveAll(x => x.PlayerId == playerId);
        }
    }
}
