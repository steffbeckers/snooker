using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.Clubs
{
    public class ClubListDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}