using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.Interclub.Seasons;

public class SeasonDivisionDto : EntityDto<Guid>
{
    public string Name { get; set; }
}