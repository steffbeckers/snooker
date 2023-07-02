using Snooker.Seasons;
using Snooker.Teams;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Divisions;

public class Division : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Division(
        Guid id,
        Guid seasonId,
        string name)
    {
        Id = id;
        SeasonId = seasonId;
        Name = name;
    }

    private Division()
    {
    }

    public int FrameCount { get; set; }

    public int? MinPlayerClass { get; set; }

    public string Name { get; set; }

    public Season Season { get; }

    public Guid SeasonId { get; set; }

    public virtual ICollection<Team> Teams { get; private set; } = new Collection<Team>();

    public Guid? TenantId { get; private set; }
}