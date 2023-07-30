using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Snooker.Interclub.Seasons;

public interface ISeasonsAppService : IApplicationService
{
    Task<SeasonDto> CopyAsync(Guid id, SeasonInputDto input);

    Task<SeasonDto> GetAsync(Guid id);
}