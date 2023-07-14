using Snooker.Interclub.Divisions;
using Snooker.Interclub.Frames;
using Snooker.Interclub.Teams;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Matches;

public class Match : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Match(
        Guid id,
        Guid homeTeamId,
        Guid awayTeamId)
    {
        Id = id;
        HomeTeamId = homeTeamId;
        AwayTeamId = awayTeamId;
    }

    private Match()
    {
    }

    public virtual Team? AwayTeam { get; }

    public Guid? AwayTeamId { get; }

    public int? AwayTeamScore { get; set; }

    public DateTime? Date { get; set; }

    public virtual Division? Division { get; set; }

    public Guid? DivisionId { get; set; }

    public virtual ICollection<Frame> Frames { get; private set; } = new Collection<Frame>();

    public virtual Team? HomeTeam { get; }

    public Guid? HomeTeamId { get; }

    public int? HomeTeamScore { get; set; }

    public virtual ICollection<MatchTeamPlayer> TeamPlayers { get; private set; } = new Collection<MatchTeamPlayer>();

    public Guid? TenantId { get; private set; }
}