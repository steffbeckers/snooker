using Snooker.Players;
using Snooker.Teams;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Matches;

public class MatchTeamPlayer : FullAuditedEntity<Guid>, IMultiTenant
{
    public MatchTeamPlayer(
        Guid id,
        Guid matchId,
        Guid teamId,
        Guid playerId)
    {
        Id = id;
        MatchId = matchId;
        TeamId = teamId;
        PlayerId = playerId;
    }

    private MatchTeamPlayer()
    {
    }

    public bool IsCaptain { get; set; }

    public virtual Match Match { get; set; }

    public Guid MatchId { get; set; }

    public virtual Player Player { get; set; }

    public Guid PlayerId { get; set; }

    public virtual Team Team { get; set; }

    public Guid TeamId { get; set; }

    public Guid? TenantId { get; private set; }
}