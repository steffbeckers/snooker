using System;

namespace Snooker.Interclub.Data.Seeding.Limburg.Contributors;

public class MatchDso
{
    public string AwayTeamName { get; set; }

    public int AwayTeamScore { get; set; }

    public DateTime Date { get; set; }

    public string HomeTeamName { get; set; }

    public int HomeTeamScore { get; set; }

    public Guid? Id { get; set; }
}