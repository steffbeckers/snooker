using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Snooker.ClubManagement.Players
{
    public class Player : FullAuditedAggregateRoot<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Guid? ClubId { get; set; }

        public Player()
        {
            // This constructor is for deserialization / ORM purpose
        }

        public Player(
            Guid id,
            [NotNull] string firstName,
            [NotNull] string lastName
        ) : base(id)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
        }

        private void SetFirstName([NotNull] string firstName)
        {
            FirstName = Check.NotNullOrWhiteSpace(
                firstName,
                nameof(firstName),
                maxLength: PlayerConsts.FirstNameMaxLength
            );
        }

        private void SetLastName([NotNull] string lastName)
        {
            LastName = Check.NotNullOrWhiteSpace(
                lastName,
                nameof(lastName),
                maxLength: PlayerConsts.LastNameMaxLength
            );
        }
    }
}
