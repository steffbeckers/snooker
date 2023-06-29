using Snooker.Addresses;
using Snooker.Players;
using Snooker.Teams;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Clubs;

public class Club : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    private string? _email;
    private string _name;
    private string? _number;
    private string? _phoneNumber;
    private string? _website;

    internal Club(
        Guid id,
        string name)
    {
        Id = id;
        Name = name;
    }

    private Club()
    {
    }

    public Address? Address { get; set; }

    public string? Email
    {
        get => _email;
        set
        {
            Check.Length(value, nameof(Email), ClubConsts.EmailMaxLength);
            _email = value;
        }
    }

    public string Name
    {
        get => _name;
        internal set
        {
            Check.NotNull(value, nameof(Name));
            Check.Length(value, nameof(Name), ClubConsts.NameMaxLength);
            _name = value;
        }
    }

    public string? Number
    {
        get => _number;
        set
        {
            Check.NotNull(value, nameof(Number));
            Check.Length(value, nameof(Number), ClubConsts.NumberMaxLength);
            _number = value;
        }
    }

    public string? PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            Check.Length(value, nameof(PhoneNumber), ClubConsts.PhoneNumberMaxLength);
            _phoneNumber = value;
        }
    }

    public virtual ICollection<Player> Players { get; set; } = new Collection<Player>();

    public virtual ICollection<Team> Teams { get; set; } = new Collection<Team>();

    public Guid? TenantId { get; private set; }

    public string? Website
    {
        get => _website;
        set
        {
            Check.Length(value, nameof(Website), ClubConsts.WebsiteMaxLength);
            _website = value;
        }
    }
}