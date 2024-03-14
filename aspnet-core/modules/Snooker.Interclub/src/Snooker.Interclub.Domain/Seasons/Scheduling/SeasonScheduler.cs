using Snooker.Interclub.Divisions;
using Snooker.Interclub.Matches;
using Snooker.Interclub.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Snooker.Interclub.Seasons.Scheduling;

public class SeasonScheduler : DomainService
{
    private IList<DateTime> _dates;
    private Season _season;
    private int[] _weekOfDate;
    private int[] _weeks;

    public async Task<Season> ScheduleAsync(Season season)
    {
        _season = season;

        await GenerateMatchesAsync();
        await GenerateDatesAndWeeksAsync();
        await SolveMatchDatesAsync();
        await ValidateAsync();

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

        _weeks = _weekOfDate.Distinct().ToArray();

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
        // TODO: This is just a test

        foreach (Match match in _season.Matches)
        {
            foreach (DateTime date in _dates)
            {
                if (match.Date.HasValue)
                {
                    continue;
                }

                if (!match.Division!.DaysOfWeek.Contains(date.DayOfWeek))
                {
                    continue;
                }

                match.Date = date;
            }
        }

        return Task.CompletedTask;
    }

    private Task ValidateAsync()
    {
        // Each match should have a date
        Match? matchWithoutDate = _season.Matches.FirstOrDefault(x => !x.Date.HasValue);
        if (matchWithoutDate != null)
        {
            throw new Exception($"Match between {matchWithoutDate.HomeTeam!.ClubTeamName} and {matchWithoutDate.AwayTeam!.ClubTeamName} is not scheduled.");
        }

        foreach (Team team in _season.Teams)
        {
            // Each team should only play max 1 time a week
            List<int> teamPlaysInWeeks = new List<int>();
            foreach (Match match in team.Matches)
            {
                int week = _weekOfDate[_dates.IndexOf(match.Date!.Value)];

                if (teamPlaysInWeeks.Contains(week))
                {
                    throw new Exception($"Team {team.ClubTeamName} already plays a match in week {week}.");
                }

                teamPlaysInWeeks.Add(week);
            }
        }

        return Task.CompletedTask;
    }
}