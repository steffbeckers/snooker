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
    private IList<(DateTime, DayOfWeek)> _dates = new List<(DateTime, DayOfWeek)>();
    private Season _season;

    public async Task<Season> ScheduleAsync(Season season)
    {
        _season = season;

        await GenerateMatchesAsync();
        await GenerateDatesAsync();
        await SolveMatchDatesAsync();

        return _season;
    }

    private Task GenerateDatesAsync()
    {
        DateTime date = _season.StartDate;

        while (date <= _season.EndDate)
        {
            _dates.Add((date, date.DayOfWeek));

            date = date.AddDays(1);
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
        CpModel model = new CpModel();

        // Create variables
        BoolVar[,] matchDateVars = new BoolVar[_season.Matches.Count, _dates.Count];

        for (int matchIndex = 0; matchIndex < _season.Matches.Count; matchIndex++)
        {
            for (int dateIndex = 0; dateIndex < _dates.Count; dateIndex++)
            {
                matchDateVars[matchIndex, dateIndex] = model.NewBoolVar($"Match_{matchIndex}_Date_{dateIndex}");
            }
        }

        // Create constraints

        // Each match should be played on date of the week defined on division level
        bool[,] matchCanBePlayedOnDate = new bool[_season.Matches.Count, _dates.Count];

        for (int matchIndex = 0; matchIndex < _season.Matches.Count; matchIndex++)
        {
            Match match = _season.Matches.ElementAt(matchIndex);

            for (int dateIndex = 0; dateIndex < _dates.Count; dateIndex++)
            {
                (DateTime, DayOfWeek) date = _dates.ElementAt(dateIndex);
                matchCanBePlayedOnDate[matchIndex, dateIndex] = match.Division!.DaysOfWeek.Contains(date.Item2);

                // TODO
                //model.Add(matchDateVars[matchIndex, dateIndex] == matchCanBePlayedOnDate[matchIndex, dateIndex]);
            }
        }

        CpSolver solver = new CpSolver();
        CpSolverStatus solverStatus = solver.Solve(model);

        if (solverStatus == CpSolverStatus.Feasible || solverStatus == CpSolverStatus.Optimal)
        {
            Logger.LogDebug($"{solverStatus} solution found");

            for (int matchIndex = 0; matchIndex < _season.Matches.Count; matchIndex++)
            {
                Match match = _season.Matches.ElementAt(matchIndex);

                for (int dateIndex = 0; dateIndex < _dates.Count; dateIndex++)
                {
                    if (solver.Value(matchDateVars[matchIndex, dateIndex]) == 1)
                    {
                        // TODO: Does this work?
                        match.Date = _dates.ElementAt(dateIndex).Item1;

                        break;
                    }
                }
            }
        }
        else
        {
            throw new Exception("No solution found");
        }

        foreach (Match match in _season.Matches.OrderBy(x => x.Date))
        {
            Logger.LogDebug($"{match.Date:yyyy-MM-dd} {match.HomeTeam!.ClubTeamName} - {match.AwayTeam!.ClubTeamName}");
        }

        return Task.CompletedTask;
    }
}