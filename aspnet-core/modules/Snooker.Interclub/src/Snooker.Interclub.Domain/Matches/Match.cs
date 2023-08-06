using Snooker.Interclub.Divisions;
using Snooker.Interclub.Frames;
using Snooker.Interclub.Teams;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Matches;

public class Match : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Match(
        Guid id,
        Team homeTeam,
        Team awayTeam)
    {
        Id = id;
        HomeTeam = homeTeam;
        HomeTeamId = homeTeam.Id;
        AwayTeam = awayTeam;
        AwayTeamId = awayTeam.Id;
    }

    protected Match()
    {
    }

    public virtual Team? AwayTeam { get; }

    public Guid? AwayTeamId { get; }

    [NotMapped]
    public virtual IList<MatchTeamPlayer> AwayTeamPlayers { get => TeamPlayers.Where(x => x.TeamId == AwayTeamId).ToList(); }

    public int? AwayTeamScore { get; set; }

    public DateTime? Date { get; set; }

    public virtual Division? Division { get; set; }

    public Guid? DivisionId { get; set; }

    public virtual ICollection<Frame> Frames { get; private set; } = new Collection<Frame>();

    public virtual Team? HomeTeam { get; }

    public Guid? HomeTeamId { get; }

    [NotMapped]
    public virtual IList<MatchTeamPlayer> HomeTeamPlayers { get => TeamPlayers.Where(x => x.TeamId == HomeTeamId).ToList(); }

    public int? HomeTeamScore { get; set; }

    public int? Round { get; set; }

    public virtual ICollection<MatchTeamPlayer> TeamPlayers { get; private set; } = new Collection<MatchTeamPlayer>();

    public Guid? TenantId { get; private set; }
}