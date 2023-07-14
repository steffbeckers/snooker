using System;
using System.Collections.Generic;

namespace Snooker.Interclub.Data.Seeding.Limburg.Contributors;

public class DivisionDso
{
    public IList<string> ClubTeamNames { get; set; } = new List<string>();

    public Guid? Id { get; set; }

    public IList<MatchDso> Matches { get; set; } = new List<MatchDso>();

    public string Name { get; set; }
}