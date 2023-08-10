using Snooker.Interclub.Clubs;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Matches;
using Snooker.Interclub.Teams;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Seasons;

public class Season : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Season(
        Guid id,
        DateTime startDate,
        DateTime endDate)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
    }

    protected Season()
    {
    }

    [NotMapped]
    public virtual ICollection<Club> Clubs { get => Teams.Select(x => x.Club).Distinct().OrderBy(x => x.Name).ToList(); }

    public virtual ICollection<Division> Divisions { get; private set; } = new Collection<Division>();

    public DateTime EndDate { get; set; }

    [NotMapped]
    public virtual ICollection<Match> Matches { get => Divisions.SelectMany(x => x.Matches).OrderBy(x => x.Date).ToList(); }

    public DateTime StartDate { get; set; }

    [NotMapped]
    public virtual ICollection<Team> Teams { get => Divisions.SelectMany(x => x.Teams).OrderBy(x => x.ClubTeamName).ToList(); }

    public Guid? TenantId { get; private set; }
}