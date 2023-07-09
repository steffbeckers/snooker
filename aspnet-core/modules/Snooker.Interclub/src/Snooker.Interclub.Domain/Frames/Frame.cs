using Snooker.Interclub.Matches;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Frames;

public class Frame : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Frame(
        Guid id,
        Guid matchId)
    {
        Id = id;
        MatchId = matchId;
    }

    private Frame()
    {
    }

    public virtual MatchTeamPlayer AwayPlayer { get; }

    public Guid AwayPlayerId { get; set; }

    public int AwayScore { get; set; }

    public virtual MatchTeamPlayer HomePlayer { get; }

    public Guid HomePlayerId { get; set; }

    public int HomeScore { get; set; }

    public virtual Match Match { get; }

    public Guid MatchId { get; private set; }

    public Guid? TenantId { get; private set; }
}