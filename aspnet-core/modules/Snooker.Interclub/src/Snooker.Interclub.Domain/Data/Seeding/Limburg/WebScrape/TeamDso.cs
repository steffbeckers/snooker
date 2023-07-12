using System.Collections.Generic;

namespace Snooker.Interclub.Data.Seeding.Limburg.WebScrape;

public class TeamDso
{
    public string Name { get; set; }

    public IList<PlayerDso> Players { get; set; } = new List<PlayerDso>();
}