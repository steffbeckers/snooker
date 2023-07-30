using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.Interclub.Seasons;

public class SeasonListDto : EntityDto<Guid>
{
    public int EndDateYear { get; set; }

    public int StartDateYear { get; set; }
}