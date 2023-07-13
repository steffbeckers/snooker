using System;

namespace Snooker.Interclub.Data.Seeding.Limburg.WebScrape;

public class PlayerDso
{
    public int? Class { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string FirstName { get; set; }

    public Guid? Id { get; set; }

    public string LastName { get; set; }
}