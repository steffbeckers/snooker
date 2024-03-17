using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Snooker.Interclub.Seasons.Scheduling;

public class SeasonScheduler : DomainService
{
    public Task<Season> ScheduleAsync(Season season)
    {
        return Task.FromResult(season);
    }
}