using Snooker.WebScraper.Addresses;
using Snooker.WebScraper.Teams;
using System.Collections.Generic;

namespace Snooker.WebScraper.Clubs;

public class Club
{
    public Address? Address { get; set; } = new Address();

    public string? Email { get; set; }

    public string Name { get; set; }

    public int Number { get; set; }

    public string? PhoneNumber { get; set; }

    public IList<Team> Teams { get; set; } = new List<Team>();

    public string? Website { get; set; }
}