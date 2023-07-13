using System;
using System.Collections.Generic;

namespace Snooker.Interclub.Data.Seeding.Limburg.Contributors;

public class DivisionDso
{
    public List<string> ClubTeamNames { get; set; } = new List<string>();

    public Guid? Id { get; set; }

    public string Name { get; set; }
}