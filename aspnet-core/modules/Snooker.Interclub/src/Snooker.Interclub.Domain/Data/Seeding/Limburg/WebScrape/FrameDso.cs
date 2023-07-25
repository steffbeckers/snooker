using System.Collections.Generic;

namespace Snooker.Interclub.Data.Seeding.Limburg.Contributors;

public class FrameDso
{
    public List<int> AwayTeamPlayerBreaks { get; set; } = new List<int>();

    public string AwayTeamPlayerName { get; set; }

    public int AwayTeamPlayerScore { get; set; }

    public List<int> HomeTeamPlayerBreaks { get; set; } = new List<int>();

    public string HomeTeamPlayerName { get; set; }

    public int HomeTeamPlayerScore { get; set; }
}