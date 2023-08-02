using Snooker.Interclub.Clubs;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Teams;
using System;
using System.Collections.Generic;
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

    public async Task<Season> ScheduleAsync(Season season)
    {
        List<Division> divisions = await AsyncExecuter.ToListAsync(season.Divisions.AsQueryable());
        List<Club> clubs = await AsyncExecuter.ToListAsync(season.Divisions.SelectMany(x => x.Teams).Select(x => x.Club).Distinct().AsQueryable());

        return season;
    }
}