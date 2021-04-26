using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Snooker.ClubManagement.Clubs.Dto
{
    public class ClubDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
