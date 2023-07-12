using System.Collections.Generic;

namespace Snooker.Interclub.Data.Seeding.Limburg.WebScrape;

public class ClubDso
{
    public AddressDso Address { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public int Number { get; set; }

    public string PhoneNumber { get; set; }

    public IList<TeamDso> Teams { get; set; } = new List<TeamDso>();

    public string Website { get; set; }
}