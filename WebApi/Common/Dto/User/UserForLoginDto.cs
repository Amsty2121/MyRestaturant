using System.ComponentModel.DataAnnotations;

namespace Common.Dto.User
{
	public class UserForLoginDto
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
