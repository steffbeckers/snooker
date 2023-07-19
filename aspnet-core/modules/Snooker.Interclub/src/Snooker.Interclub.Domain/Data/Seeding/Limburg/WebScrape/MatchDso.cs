using System;
using System.Collections.Generic;

namespace Snooker.Interclub.Data.Seeding.Limburg.Contributors;

public class MatchDso
{
    public string AwayTeamName { get; set; }

    public IList<string> AwayTeamPlayerNames { get; set; } = new List<string>();

    public int AwayTeamScore { get; set; }

    public DateTime Date { get; set; }

    public string? DetailId { get; set; }

    public IList<FrameDso> Frames { get; set; } = new List<FrameDso>();

    public string HomeTeamName { get; set; }

    public IList<string> HomeTeamPlayerNames { get; set; } = new List<string>();

    public int HomeTeamScore { get; set; }

    public Guid? Id { get; set; }
}