using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Snooker.Interclub.Seasons;

[RemoteService(IsEnabled = false)]
public class SeasonsAppService : ApplicationService, ISeasonsAppService
{
    private readonly SeasonManager _seasonManager;
    private readonly ISeasonRepository _seasonRepository;

    public SeasonsAppService(
        SeasonManager seasonManager,
        ISeasonRepository seasonRepository)
    {
        _seasonManager = seasonManager;
        _seasonRepository = seasonRepository;
    }

    public async Task<SeasonDto> CopyAsync(Guid id, SeasonInputDto input)
    {
        Season season = await _seasonManager.CopyAsync(
            seasonToCopyId: id,
            id: GuidGenerator.Create(),
            startDate: new DateTime(input.StartDateYear, 1, 1),
            endDate: new DateTime(input.EndDateYear, 1, 1));

        season = await _seasonRepository.InsertAsync(season);

        await CurrentUnitOfWork.SaveChangesAsync();

        return await GetAsync(season.Id);
    }

    public async Task<SeasonDto> GetAsync(Guid id)
    {
        Season season = await _seasonRepository.GetAsync(id);

        return ObjectMapper.Map<Season, SeasonDto>(season);
    }
}