using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Leagues;

public class League : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    internal League(
        Guid id,
        string name)
    {
        Id = id;
        Name = name;
    }

    private League()
    {
    }

    public string Name { get; internal set; }

    public Guid? TenantId { get; private set; }
}