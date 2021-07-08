using System;
using System.ComponentModel.DataAnnotations;

namespace Snooker.Players
{
    public class PlayerUpdateDto
    {
        [Required]
        [StringLength(PlayerConsts.FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(PlayerConsts.LastNameMaxLength)]
        public string LastName { get; set; }

        public Guid? UserId { get; set; }
    }
}