using Snooker.Players;
using Snooker.Teams;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.TeamPlayers;

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

    public virtual Player Player { get; set; }

    public Guid PlayerId { get; set; }

    public virtual Team Team { get; set; }

    public Guid TeamId { get; set; }

    public Guid? TenantId { get; set; }
}