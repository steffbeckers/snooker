using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Snooker.Players
{
    public class Player : FullAuditedAggregateRoot<Guid>
    {
        private string _firstName;
        private string _lastName;

        public virtual string FirstName
        {
            get => _firstName;
            set
            {
                Check.NotNull(value, nameof(FirstName));
                Check.Length(value, nameof(FirstName), PlayerConsts.FirstNameMaxLength, 0);
                _firstName = value;
            }
        }

        public virtual string LastName
        {
            get => _lastName;
            set
            {
                Check.NotNull(value, nameof(LastName));
                Check.Length(value, nameof(LastName), PlayerConsts.LastNameMaxLength, 0);
                _lastName = value;
            }
        }

        public virtual Guid? UserId { get; set; }

        public Player(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        private Player()
        {
        }
    }
}