using System;
using System.ComponentModel.DataAnnotations;

namespace Snooker.ClubPlayers
{
    public class ClubPlayerUpdateDto
    {
        [Required]
        public Guid ClubId { get; set; }

        [Required]
        public Guid PlayerId { get; set; }
    }
}