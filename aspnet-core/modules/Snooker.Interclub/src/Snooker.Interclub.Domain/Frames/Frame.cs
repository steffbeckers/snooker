using Snooker.Interclub.Matches;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Frames;

public class Frame : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Frame(
        Guid id,
        Match match,
        MatchTeamPlayer homePlayer,
        MatchTeamPlayer awayPlayer)
    {
        Id = id;
        Match = match;
        MatchId = match.Id;
        HomePlayer = homePlayer;
        HomePlayerId = homePlayer.Id;
        AwayPlayer = awayPlayer;
        AwayPlayerId = awayPlayer.Id;
    }

    private Frame()
    {
    }

    public virtual MatchTeamPlayer? AwayPlayer { get; set; }

    public Guid? AwayPlayerId { get; set; }

    public int? AwayPlayerScore { get; set; }

    public virtual MatchTeamPlayer? HomePlayer { get; set; }

    public Guid? HomePlayerId { get; set; }

    public int? HomePlayerScore { get; set; }

    public virtual Match Match { get; set; }

    public Guid MatchId { get; private set; }

    public Guid? TenantId { get; private set; }
}