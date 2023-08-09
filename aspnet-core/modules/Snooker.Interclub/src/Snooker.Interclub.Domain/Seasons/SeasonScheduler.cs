using Google.OrTools.ConstraintSolver;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Matches;
using Snooker.Interclub.Teams;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Snooker.Interclub.Seasons;

public class SeasonScheduler : DomainService
{
    public async Task<Season> ScheduleAsync(Season season)
    {
        await GenerateAllMatchesAsync(season);
        await GenerateScheduleAsync(season);

        return season;
    }

    private Task GenerateAllMatchesAsync(Season season)
    {
        // Generate all matches of season
        foreach (Division division in season.Divisions)
        {
            foreach (Team homeTeam in division.Teams)
            {
                foreach (Team awayTeam in division.Teams)
                {
                    // Teams can't play themselves
                    if (homeTeam.Id == awayTeam.Id)
                    {
                        continue;
                    }

                    // Per round, 1 team only plays 1 match against each other team
                    Match? match = division.Matches.FirstOrDefault(x => x.HomeTeamId == awayTeam.Id && x.AwayTeamId == homeTeam.Id);

                    if (match != null)
                    {
                        continue;
                    }

                    match = new Match(
                        GuidGenerator.Create(),
                        homeTeam,
                        awayTeam)
                    {
                        Division = division,
                        Round = 1
                    };

                    division.Matches.Add(match);
                }
            }

            if (division.RoundsDuringSeason < 2)
            {
                continue;
            }

            for (int round = 2; round <= division.RoundsDuringSeason; round++)
            {
                foreach (Match previousMatch in division.Matches.Where(x => x.Round == round - 1).ToList())
                {
                    Match match = new Match(
                        GuidGenerator.Create(),
                        previousMatch.AwayTeam!,
                        previousMatch.HomeTeam!)
                    {
                        Division = division,
                        Round = round
                    };

                    division.Matches.Add(match);
                }
            }
        }

        return Task.CompletedTask;
    }

    private Task GenerateScheduleAsync(Season season)
    {
        Solver solver = new Solver("SeasonScheduler");

        int totalSeasonDays = 240; // Total days in the season
        IntVar[,,] matches = new IntVar[season.Matches.Count, totalSeasonDays, 1];

        solver.EndSearch();

        return Task.CompletedTask;
    }
}