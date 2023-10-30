using Google.OrTools.Sat;
using Microsoft.Extensions.Logging;
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
    private int[] _weeks;
    private Season _season;

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

        _weeks = new int[_dates.Count];

        int week = _dates[0].DayOfWeek == DayOfWeek.Monday ? 0 : 1;
        for (int i = 0; i < _dates.Count; i++)
        {
            if (_dates[i].DayOfWeek == DayOfWeek.Monday)
            {
                week++;
            }

            _weeks[i] = week;
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

    // This method solves the match dates of all the matches of the season using Google OR-Tools.
    // It uses the following constraints:
    // - Each match should be played on a day of the week that is specified on division level (match.Division.DaysOfWeek)
    // - If a home team has a preference for a specific day of the week (match.HomeTeam.PreferredMatchDay), the match should be played on that day
    private Task SolveMatchDatesAsync()
    {
        CpModel model = new CpModel();

        // Create variables
        Dictionary<Guid, IntVar> matchDateVars = new Dictionary<Guid, IntVar>();

        foreach (Match match in _season.Matches)
        {
            matchDateVars.Add(match.Id, model.NewIntVar(0, _dates.Count - 1, $"Match_{match.Id}_Date"));
        }

        // Create constraints
        foreach (Match match in _season.Matches)
        {
            foreach (DateTime date in _dates)
            {
                // Each match should be played on a day of the week that is specified on division level (match.Division.DaysOfWeek)
                if (!match.Division!.DaysOfWeek.Contains(date.DayOfWeek))
                {
                    model.Add(matchDateVars[match.Id] != _dates.IndexOf(date));
                }

                // If a home team has a preference for a specific day of the week (match.HomeTeam.PreferredMatchDay), the match should be played on that day
                if (match.HomeTeam!.PreferredMatchDay.HasValue && match.HomeTeam.PreferredMatchDay.Value != date.DayOfWeek)
                {
                    model.Add(matchDateVars[match.Id] != _dates.IndexOf(date));
                }
            }
        }

        CpSolver solver = new CpSolver();
        CpSolverStatus solverStatus = solver.Solve(model);

        if (solverStatus == CpSolverStatus.Feasible || solverStatus == CpSolverStatus.Optimal)
        {
            Logger.LogDebug($"{solverStatus} solution found");

            foreach (Match match in _season.Matches)
            {
                match.Date = _dates[(int)solver.Value(matchDateVars[match.Id])];
            }
        }
        else
        {
            throw new Exception("No solution found");
        }

        foreach (Match match in _season.Matches.OrderByDescending(x => x.Date))
        {
            Logger.LogDebug($"{match.Date:yyyy-MM-dd} {match.HomeTeam!.ClubTeamName} - {match.AwayTeam!.ClubTeamName}");
        }

        return Task.CompletedTask;
    }
}