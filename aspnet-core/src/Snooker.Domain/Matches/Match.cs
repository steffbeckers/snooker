using Snooker.Teams;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Matches;

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

    public virtual List<MatchTeamPlayer> AwayTeamPlayers { get => TeamPlayers.Where(x => x.TeamId == AwayTeamId).ToList(); }

    public DateTime? Date { get; set; }

    public virtual Team? HomeTeam { get; }

    public Guid? HomeTeamId { get; }

    public virtual List<MatchTeamPlayer> HomeTeamPlayers { get => TeamPlayers.Where(x => x.TeamId == HomeTeamId).ToList(); }

    public virtual ICollection<MatchTeamPlayer> TeamPlayers { get; set; } = new Collection<MatchTeamPlayer>();

    public Guid? TenantId { get; private set; }
}