using Google.OrTools.Sat;
using Snooker.Interclub.Clubs;
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
    private Dictionary<Guid, IList<DateTime>> _availableMatchDatesPerDivision = new Dictionary<Guid, IList<DateTime>>();
    private Dictionary<Guid, Dictionary<DateTime, int>> _availableTablesPerMatchDatePerClub = new Dictionary<Guid, Dictionary<DateTime, int>>();
    private Season _season;
    private Dictionary<Guid, Dictionary<DateTime, int>> _weekOfAvailableMatchDatesPerDivision = new Dictionary<Guid, Dictionary<DateTime, int>>();

    public async Task<Season> ScheduleAsync(Season season)
    {
        _season = season;

        await GenerateMatchesAsync();
        await GenerateAvailableMatchDatesPerDivisionAsync();
        await GenerateWeekOfAvailableMatchDatesPerDivisionAsync();
        await GenerateAvailableTablesPerMatchDatePerClubAsync();
        await SolveMatchDatesAsync();

        return _season;
    }

    private Task GenerateAvailableMatchDatesPerDivisionAsync()
    {
        _availableMatchDatesPerDivision = new Dictionary<Guid, IList<DateTime>>();

        foreach (Division division in _season.Divisions)
        {
            _availableMatchDatesPerDivision.Add(division.Id, new List<DateTime>());

            DateTime currentDate = _season.StartDate;

            while (currentDate <= _season.EndDate)
            {
                if (division.DaysOfWeek.Contains(currentDate.DayOfWeek))
                {
                    _availableMatchDatesPerDivision[division.Id].Add(currentDate);
                }

                currentDate = currentDate.AddDays(1);
            }
        }

        return Task.CompletedTask;
    }

    private Task GenerateAvailableTablesPerMatchDatePerClubAsync()
    {
        _availableTablesPerMatchDatePerClub = new Dictionary<Guid, Dictionary<DateTime, int>>();

        foreach (Club club in _season.Clubs)
        {
            _availableTablesPerMatchDatePerClub.Add(club.Id, new Dictionary<DateTime, int>());

            foreach (DateTime matchDate in _availableMatchDatesPerDivision.SelectMany(x => x.Value).Distinct())
            {
                _availableTablesPerMatchDatePerClub[club.Id].Add(matchDate, club.NumberOfTables);
            }
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

    private Task GenerateWeekOfAvailableMatchDatesPerDivisionAsync()
    {
        _weekOfAvailableMatchDatesPerDivision = new Dictionary<Guid, Dictionary<DateTime, int>>();

        foreach (Division division in _season.Divisions)
        {
            _weekOfAvailableMatchDatesPerDivision.Add(division.Id, new Dictionary<DateTime, int>());

            List<DateTime> dateTimes = _availableMatchDatesPerDivision[division.Id].ToList();
            int week = 1;

            foreach (DateTime matchDate in dateTimes)
            {
                _weekOfAvailableMatchDatesPerDivision[division.Id].Add(matchDate, week);

                if (division.DaysOfWeek.Last() == matchDate.DayOfWeek)
                {
                    week++;
                }
            }
        }

        return Task.CompletedTask;
    }

    private Task SolveMatchDatesAsync()
    {
        CpModel model = new CpModel();

        // Create variables

        // Match date per match
        Dictionary<Guid, IntVar> matchDateVars = new Dictionary<Guid, IntVar>();

        foreach (Match match in _season.Matches)
        {
            matchDateVars.Add(match.Id, model.NewIntVar(0, _availableMatchDatesPerDivision[match.Division!.Id].Count - 1, $"MatchDate_{match.Id}"));
        }

        // Solve
        CpSolver solver = new CpSolver();
        CpSolverStatus solverStatus = solver.Solve(model);

        // Check solution
        if (solverStatus == CpSolverStatus.Feasible || solverStatus == CpSolverStatus.Optimal)
        {
            // Set match dates
            foreach (Match match in _season.Matches)
            {
                match.Date = _availableMatchDatesPerDivision[match.Division!.Id][(int)solver.Value(matchDateVars[match.Id])];
            }
        }
        else
        {
            throw new Exception("No solution found.");
        }

        // Print matches
        foreach (Match match in _season.Matches.OrderBy(x => x.Date))
        {
            Console.WriteLine($"{match.Date:yyyy-MM-dd} {match.HomeTeam!.ClubTeamName} - {match.AwayTeam!.ClubTeamName}");
        }

        return Task.CompletedTask;
    }
}