using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Division;

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

    public int? MinPlayerClass { get; set; }

    public string Name { get; set; }

    public Guid? TenantId { get; private set; }
}