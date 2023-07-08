using Snooker.WebScraper.Players;
using System.Collections.Generic;

namespace Snooker.WebScraper.Teams;

public class Team
{
    public string Name { get; set; }

    public IList<Player> Players { get; set; } = new List<Player>();
}