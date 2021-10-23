using Common.Dto.Kitcheners;
using Domain.Entities;
using Domain.Entities.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Kitcheners.Commands.RegisterKitchener
{
	public class RegisterKitchenerCommand : IRequest<IdentityResult>
	{
		public InsertKitchenerDto Dto { get; set; }
		public UserManager<User> UserManager { get; set; }
	}

	public class RegisterKitchenerCommandHandler : IRequestHandler<RegisterKitchenerCommand, IdentityResult>
	{
		public async Task<IdentityResult> Handle(RegisterKitchenerCommand request, CancellationToken cancellationToken)
		{
			var user = new User()
			{
				UserName = request.Dto.Username,
				UserDetails = new UserDetails()
				{
					FirstName = request.Dto.FirstName,
					LastName = request.Dto.LastName,
                    Kitchener = new Kitchener()
				}
			};
			var userInsertion = await request.UserManager.CreateAsync(user, request.Dto.Password);

			if (userInsertion.Succeeded)
			{
				var kitchenerInsertion = await request.UserManager.AddToRoleAsync(user, "kitchener");

				if (kitchenerInsertion.Succeeded)
				{
					return IdentityResult.Success;
				}

			}
			return IdentityResult.Failed();
		}
	}
}
