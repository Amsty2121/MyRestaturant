using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Authorization
{
	public class User : IdentityUser<int>
	{
		public UserDetails UserDetails { get; set; }
	}
}
