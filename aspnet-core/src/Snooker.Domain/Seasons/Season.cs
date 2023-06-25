using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Seasons;

public class Season : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    internal Season(
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

    public DateTime EndDate { get; private set; }

    public DateTime StartDate { get; private set; }

    public Guid? TenantId { get; private set; }
}