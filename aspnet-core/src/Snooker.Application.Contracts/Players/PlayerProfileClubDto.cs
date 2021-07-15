using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.Players
{
    public class PlayerProfileClubDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}