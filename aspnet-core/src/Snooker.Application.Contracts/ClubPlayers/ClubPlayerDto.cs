using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.ClubPlayers
{
    public class ClubPlayerDto : FullAuditedEntityDto<Guid>
    {
        public Guid ClubId { get; set; }
        public Guid PlayerId { get; set; }
    }
}