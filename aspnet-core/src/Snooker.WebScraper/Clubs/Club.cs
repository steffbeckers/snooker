using Snooker.WebScraper.Addresses;
using Snooker.WebScraper.Players;

namespace Snooker.WebScraper.Clubs;

public class Club
{
    public Address? Address { get; set; } = new Address();

    public string? Email { get; set; }

    public string Name { get; set; }

    public int Number { get; set; }

    public string? PhoneNumber { get; set; }

    public IList<Player> Players { get; set; } = new List<Player>();

    public string? Website { get; set; }
}