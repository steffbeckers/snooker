using System;

namespace Snooker.WebScraper.Players;

public class Player
{
    public int? Class { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string FirstName { get; set; }

    //public string? Image { get; set; }

    public string LastName { get; set; }
}