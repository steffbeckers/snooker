using Snooker.WebScraper.Addresses;

namespace Snooker.WebScraper.Clubs;

public class Club
{
    public Address? Address { get; set; } = new Address();

    public string? Email { get; set; }

    public string Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Website { get; set; }
}