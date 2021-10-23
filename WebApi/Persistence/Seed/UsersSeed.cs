using Domain.Entities.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Seed
{
	public static class UsersSeed
	{
		public static async Task Seed(UserManager<User> userManager)
		{
			if (!userManager.Users.Any())
			{
				var user = new User()
				{
					UserName = "admin",
				};

				var userInsertion = await userManager.CreateAsync(user, "Qwerty1!");

				if (userInsertion.Succeeded)
				{
					await userManager.AddToRoleAsync(user, "admin");
				}
			}
		}
	}
}
