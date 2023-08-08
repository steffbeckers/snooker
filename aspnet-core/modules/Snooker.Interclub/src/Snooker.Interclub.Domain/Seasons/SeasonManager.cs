using Snooker.Interclub.Divisions;
using Snooker.Interclub.Matches;
using Snooker.Interclub.Teams;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Snooker.Interclub.Seasons;

public class SeasonManager : DomainService
{
    private readonly ISeasonRepository _seasonRepository;

    public SeasonManager(ISeasonRepository seasonRepository)
    {
        _seasonRepository = seasonRepository;
    }

    public async Task<Season> CopyAsync(Guid seasonToCopyId, Guid id, DateTime startDate, DateTime endDate)
    {
        Season season = new Season(id, startDate, endDate);
        Season seasonToCopy = await _seasonRepository.GetAsync(seasonToCopyId);

        foreach (Division divisionToCopy in seasonToCopy.Divisions)
        {
            Division division = new Division(
                GuidGenerator.Create(),
                season,
                divisionToCopy.Name)
            {
                FrameCount = divisionToCopy.FrameCount,
                MinPlayerClass = divisionToCopy.MinPlayerClass,
                SortOrder = divisionToCopy.SortOrder
            };

            foreach (Team teamToCopy in divisionToCopy.Teams)
            {
                Team team = new Team(
                    GuidGenerator.Create(),
                    division,
                    teamToCopy.Club,
                    teamToCopy.Name);

                division.Teams.Add(team);
            }

            season.Divisions.Add(division);
        }

        return season;
    }

    public Task<Season> ScheduleAsync(Season season)
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
                    Match match = division.Matches.FirstOrDefault(x => x.HomeTeamId == awayTeam.Id && x.AwayTeamId == homeTeam.Id);

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

            if (division.RoundsPerSeasonCount < 2)
            {
                continue;
            }

            for (int round = 2; round <= division.RoundsPerSeasonCount; round++)
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

        return Task.FromResult(season);
    }
}