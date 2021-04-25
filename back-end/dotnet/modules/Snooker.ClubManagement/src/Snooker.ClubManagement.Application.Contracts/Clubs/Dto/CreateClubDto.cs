using Snooker.ClubManagement.Clubs;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Snooker.ClubManagement.Clubs.Dto
{
    public class CreateClubDto : EntityDto<Guid?>
    {
        [Required]
        [StringLength(ClubConsts.NameMaxLength)]
        public string Name { get; set; }
    }
}
