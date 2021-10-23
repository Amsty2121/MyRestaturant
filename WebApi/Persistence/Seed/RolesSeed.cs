using Domain.Entities.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Persistence.Seed
{
	public class RolesSeed
	{
		public static async Task Seed(RoleManager<Role> roleManager)
		{
			string[] roleNames = { "waiter", "kitchener", "admin" };

			foreach (var roleName in roleNames)
			{
				var roleExist = await roleManager.RoleExistsAsync(roleName);
				if (!roleExist)
				{
					await roleManager.CreateAsync(new Role() { Name = roleName });
				}
			}
		}
	}
}
