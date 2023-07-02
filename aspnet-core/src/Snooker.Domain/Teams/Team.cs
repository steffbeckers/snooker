using Snooker.Clubs;
using Snooker.Divisions;
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
        // TODO
        //Guid divisionId,
        Guid clubId,
        string name)
    {
        Id = id;
        // TODO
        //DivisionId = divisionId;
        ClubId = clubId;
        Name = name;
    }

    private Team()
    {
    }

    public virtual Club Club { get; }

    public Guid ClubId { get; set; }

    public virtual Division? Division { get; }

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

    public virtual ICollection<TeamPlayer> Players { get; private set; } = new Collection<TeamPlayer>();

    public Guid? TenantId { get; private set; }
}