using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto.Admins
{
	public class InsertAdminDto
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid Firstname length")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid Lastname length")]
		public string LastName { get; set; }
    }
}
