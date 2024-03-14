using Snooker.Interclub.Divisions;
using Snooker.Interclub.Matches;
using Snooker.Interclub.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Snooker.Interclub.Seasons;

public class SeasonScheduler : DomainService
{
    private IList<DateTime> _dates;
    private Season _season;
    private int[] _weekOfDate;

    public async Task<Season> ScheduleAsync(Season season)
    {
        _season = season;

        await GenerateMatchesAsync();
        await GenerateDatesAndWeeksAsync();
        await SolveMatchDatesAsync();

        return _season;
    }

    private Task GenerateDatesAndWeeksAsync()
    {
        _dates = new List<DateTime>();

        DateTime date = _season.StartDate;
        while (date <= _season.EndDate)
        {
            _dates.Add(date);

            date = date.AddDays(1);
        }

        _weekOfDate = new int[_dates.Count];

        int week = _dates[0].DayOfWeek == DayOfWeek.Monday ? 0 : 1;
        for (int i = 0; i < _dates.Count; i++)
        {
            if (_dates[i].DayOfWeek == DayOfWeek.Monday)
            {
                week++;
            }

            _weekOfDate[i] = week;
        }

        return Task.CompletedTask;
    }

    private Task GenerateMatchesAsync()
    {
        // Generate all matches of season
        foreach (Division division in _season.Divisions)
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

    private Task SolveMatchDatesAsync()
    {
        return Task.CompletedTask;
    }
}