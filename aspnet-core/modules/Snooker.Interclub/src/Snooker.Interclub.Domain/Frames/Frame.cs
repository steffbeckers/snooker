using Snooker.Interclub.Matches;
using Snooker.Interclub.Players;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Frames;

public class Frame : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Frame(
        Guid id,
        Match match,
        Player homePlayer,
        Player awayPlayer)
    {
        Id = id;
        Match = match;
        MatchId = match.Id;
        HomePlayer = homePlayer;
        HomePlayerId = homePlayer.Id;
        AwayPlayer = awayPlayer;
        AwayPlayerId = awayPlayer.Id;
    }

    protected Frame()
    {
    }

    public virtual Player? AwayPlayer { get; set; }

    [NotMapped]
    public virtual IList<Break> AwayPlayerBreaks { get => Breaks.Where(x => x.PlayerId == AwayPlayerId).ToList(); }

    public Guid? AwayPlayerId { get; set; }

    public int? AwayPlayerScore { get; set; }

    public virtual ICollection<Break> Breaks { get; set; } = new Collection<Break>();

    public virtual Player? HomePlayer { get; set; }

    [NotMapped]
    public virtual IList<Break> HomePlayerBreaks { get => Breaks.Where(x => x.PlayerId == HomePlayerId).ToList(); }

    public Guid? HomePlayerId { get; set; }

    public int? HomePlayerScore { get; set; }

    public virtual Match Match { get; set; }

    public Guid MatchId { get; private set; }

    public Guid? TenantId { get; private set; }
}