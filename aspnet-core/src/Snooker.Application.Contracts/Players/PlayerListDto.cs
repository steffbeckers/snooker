using System;
using Volo.Abp.Application.Dtos;

namespace Snooker.Players
{
    public class PlayerListDto : EntityDto<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}