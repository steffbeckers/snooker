using System;
using System.ComponentModel.DataAnnotations;

namespace Snooker.Clubs
{
    public class ClubCreateDto
    {
        [Required]
        [StringLength(ClubConsts.NameMaxLength)]
        public string Name { get; set; }
    }
}