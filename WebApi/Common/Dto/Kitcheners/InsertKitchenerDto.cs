using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto.Kitcheners
{
	public class InsertKitchenerDto
	{
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid Kitchener's username length")]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid Kitchener's Firstname length")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid Kitchener's Lastname length")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid password length")]
        public string Password { get; set; }
    }
}
