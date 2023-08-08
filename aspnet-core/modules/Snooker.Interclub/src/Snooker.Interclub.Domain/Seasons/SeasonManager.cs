using Snooker.Interclub.Divisions;
using Snooker.Interclub.Teams;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Snooker.Interclub.Seasons;

public class SeasonManager : DomainService
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly SeasonScheduler _seasonScheduler;

    public SeasonManager(
        ISeasonRepository seasonRepository,
        SeasonScheduler seasonScheduler)
    {
        _seasonRepository = seasonRepository;
        _seasonScheduler = seasonScheduler;
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

    public async Task<Season> ScheduleAsync(Guid id)
    {
        Season season = await _seasonRepository.GetAsync(id);

        return await _seasonScheduler.ScheduleAsync(season);
    }
}