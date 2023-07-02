using Snooker.Players;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Teams;

public class TeamPlayer : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public TeamPlayer(
        Guid id,
        Guid teamId,
        Guid playerId)
    {
        Id = id;
        TeamId = teamId;
        PlayerId = playerId;
    }

    private TeamPlayer()
    {
    }

    public bool IsCaptain { get; set; }

    public virtual Player Player { get; }

    public Guid PlayerId { get; set; }

    public virtual Team Team { get; }

    public Guid TeamId { get; set; }

    public Guid? TenantId { get; set; }
}