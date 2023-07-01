using Snooker.Clubs;
using Snooker.Divisions;
using Snooker.TeamPlayers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Teams;

public class Team : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    private string _name;

    public Team(
        Guid id,
        string name)
    {
        Id = id;
        Name = name;
    }

    private Team()
    {
    }

    public Club? Club { get; set; }

    public Guid? ClubId { get; set; }

    public Division? Division { get; set; }

    public Guid? DivisionId { get; set; }

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

    public ICollection<TeamPlayer> Players { get; set; } = new Collection<TeamPlayer>();

    public Guid? TenantId { get; private set; }
}