using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.Players
{
    public class PlayerProfileDto : EntityDto<Guid>
    {
        public PlayerProfileClubDto Club { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}