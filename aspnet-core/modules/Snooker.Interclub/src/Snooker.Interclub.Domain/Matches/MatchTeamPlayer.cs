using Snooker.Interclub.Players;
using Snooker.Interclub.Teams;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Matches;

public class MatchTeamPlayer : FullAuditedEntity<Guid>, IMultiTenant
{
    public MatchTeamPlayer(
        Guid id,
        Match match,
        Team team,
        Player player)
    {
        Id = id;
        Match = match;
        MatchId = match.Id;
        Team = team;
        TeamId = team.Id;
        Player = player;
        PlayerId = player.Id;
    }

    protected MatchTeamPlayer()
    {
    }

    public bool IsCaptain { get; set; }

    public virtual Match Match { get; }

    public Guid MatchId { get; set; }

    public virtual Player Player { get; }

    public Guid PlayerId { get; set; }

    public virtual Team Team { get; }

    public Guid TeamId { get; set; }

    public Guid? TenantId { get; private set; }
}