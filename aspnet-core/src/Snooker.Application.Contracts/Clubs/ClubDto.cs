using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.Clubs
{
    public class ClubDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}