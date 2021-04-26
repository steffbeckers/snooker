using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Snooker.ClubManagement.Clubs
{
    public class Club : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }

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
    }
}
