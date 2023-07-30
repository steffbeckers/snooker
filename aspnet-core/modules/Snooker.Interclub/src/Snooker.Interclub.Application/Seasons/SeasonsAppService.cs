using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

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

    public async Task<List<SeasonListDto>> GetAllAsync()
    {
        IQueryable<Season> seasonQueryable = await _seasonRepository.GetQueryableAsync();

        return await AsyncExecuter.ToListAsync(
            ObjectMapper.GetMapper()
                .ProjectTo<SeasonListDto>(seasonQueryable));
    }

    public async Task<SeasonDto> GetAsync(Guid id)
    {
        IQueryable<Season> seasonQueryable = await _seasonRepository.GetQueryableAsync();

        return await AsyncExecuter.FirstOrDefaultAsync(
            ObjectMapper.GetMapper()
                .ProjectTo<SeasonDto>(seasonQueryable)
                .Where(x => x.Id == id));
    }
}