using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Snooker.Interclub.Seasons;

public class SeasonDto : EntityDto<Guid>
{
    public List<SeasonDivisionDto> Divisions { get; set; } = new List<SeasonDivisionDto>();

    public int EndDateYear { get; set; }

    public int StartDateYear { get; set; }
}