using Snooker.Interclub.Divisions;
using Snooker.Interclub.Teams;
using System;
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
        // TODO
        return Task.FromResult(season);
    }
}