using Snooker.Interclub.Clubs;
using Snooker.Interclub.Divisions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Teams;

public class Team : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    private string _name;

    public Team(
        Guid id,
        Division division,
        Club club,
        string name)
    {
        Id = id;
        Division = division;
        DivisionId = division.Id;
        Club = club;
        ClubId = club.Id;
        Name = name;
    }

    protected Team()
    {
    }

    public virtual Club Club { get; }

    public Guid ClubId { get; set; }

    public string ClubTeamName { get => $"{Club.Name} {Name}"; }

    public virtual Division Division { get; }

    public Guid DivisionId { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            Check.NotNull(value, nameof(Name));
            Check.Length(value, nameof(Name), TeamConsts.NameMaxLength);
            _name = value;
        }
    }

    public virtual ICollection<TeamPlayer> Players { get; private set; } = new Collection<TeamPlayer>();

    public Guid? TenantId { get; private set; }
}