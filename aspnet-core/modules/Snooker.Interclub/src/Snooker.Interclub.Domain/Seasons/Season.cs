using Snooker.Interclub.Divisions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Seasons;

public class Season : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Season(
        Guid id,
        DateTime startDate,
        DateTime endDate)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
    }

    private Season()
    {
    }

    public virtual ICollection<Division> Divisions { get; private set; } = new Collection<Division>();

    public DateTime EndDate { get; set; }

    public DateTime StartDate { get; set; }

    public Guid? TenantId { get; private set; }
}