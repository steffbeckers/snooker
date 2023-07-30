using Snooker.Interclub.Matches;
using Snooker.Interclub.Seasons;
using Snooker.Interclub.Teams;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Divisions;

public class Division : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Division(
        Guid id,
        Season season,
        string name)
    {
        Id = id;
        Season = season;
        SeasonId = season.Id;
        Name = name;
    }

    protected Division()
    {
    }

    public int? FrameCount { get; set; }

    public virtual ICollection<Match> Matches { get; private set; } = new Collection<Match>();

    public int? MinPlayerClass { get; set; }

    public string Name { get; set; }

    public virtual Season Season { get; }

    public Guid SeasonId { get; set; }

    public int? SortOrder { get; set; }

    public virtual ICollection<Team> Teams { get; private set; } = new Collection<Team>();

    public Guid? TenantId { get; private set; }
}