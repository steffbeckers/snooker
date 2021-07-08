using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.Players
{
    public class PlayerDto : FullAuditedEntityDto<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? UserId { get; set; }
    }
}