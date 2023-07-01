using Snooker.Teams;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Divisions;

public class Division : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    internal Division(
        Guid id,
        string name)
    {
        Id = id;
        Name = name;
    }

    private Division()
    {
    }

    public int FrameCount { get; set; }

    public int? MinPlayerClass { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Team> Teams { get; set; } = new Collection<Team>();

    public Guid? TenantId { get; private set; }
}