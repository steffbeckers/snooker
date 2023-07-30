using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace Snooker.Interclub.Seasons;

[RemoteService(Name = InterclubRemoteServiceConsts.RemoteServiceName)]
[Area(InterclubRemoteServiceConsts.ModuleName)]
[ControllerName("Interclub - Seasons")]
[Route("api/interclub/seasons")]
public class SeasonsController : InterclubController, ISeasonsAppService
{
    private readonly ISeasonsAppService _seasonsAppService;

    public SeasonsController(ISeasonsAppService seasonsAppService)
    {
        _seasonsAppService = seasonsAppService;
    }

    [HttpPost]
    [Route("{id}/copy")]
    public Task<SeasonDto> CopyAsync(Guid id, SeasonInputDto input)
    {
        return _seasonsAppService.CopyAsync(id, input);
    }

    [HttpGet]
    public Task<List<SeasonListDto>> GetAllAsync()
    {
        return _seasonsAppService.GetAllAsync();
    }

    [HttpGet]
    [Route("{id}")]
    public Task<SeasonDto> GetAsync(Guid id)
    {
        return _seasonsAppService.GetAsync(id);
    }
}