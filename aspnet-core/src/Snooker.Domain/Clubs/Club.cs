using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Snooker.Clubs
{
    public class Club : FullAuditedAggregateRoot<Guid>
    {
        private string _name;

        public virtual string Name
        {
            get => _name;
            set
            {
                Check.NotNull(value, nameof(Name));
                Check.Length(value, nameof(Name), ClubConsts.NameMaxLength, 0);
                _name = value;
            }
        }

        public Club(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        private Club()
        {
        }
    }
}