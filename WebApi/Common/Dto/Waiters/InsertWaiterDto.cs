using System.ComponentModel.DataAnnotations;

namespace Common.Dto.Waiters
{
	public class InsertWaiterDto
	{
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid Waiter's username length")]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid Waiter's first name length")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid last name length")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid Waiter's password length")]
        public string Password { get; set; }
    }
}
