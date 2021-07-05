using System.ComponentModel.DataAnnotations;

namespace Snooker.Clubs
{
    public class ClubUpdateDto
    {
        [Required]
        [StringLength(ClubConsts.NameMaxLength)]
        public string Name { get; set; }
    }
}