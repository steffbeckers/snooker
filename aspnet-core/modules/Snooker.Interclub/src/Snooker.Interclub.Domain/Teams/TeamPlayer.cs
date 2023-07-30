using Snooker.Interclub.Players;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Teams;

public class TeamPlayer : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public TeamPlayer(
        Guid id,
        Team team,
        Player player)
    {
        Id = id;
        Team = team;
        TeamId = team.Id;
        Player = player;
        PlayerId = player.Id;
    }

    protected TeamPlayer()
    {
    }

    public bool IsCaptain { get; set; }

    public virtual Player Player { get; }

    public Guid PlayerId { get; set; }

    public virtual Team Team { get; }

    public Guid TeamId { get; set; }

    public Guid? TenantId { get; set; }
}