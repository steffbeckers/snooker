using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.Clubs
{
    public class ClubSimpleDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}