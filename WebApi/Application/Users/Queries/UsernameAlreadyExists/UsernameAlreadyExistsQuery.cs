using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Authorization;

namespace Application.Users.Queries.UsernameAlreadyExists
{
	public class UsernameAlreadyExistsQuery : IRequest<bool>
	{
		public string Username { get; set; }
		public UserManager<User> UserManager { get; set; }
	}

	public class UsernameAlreadyExistsQueryHandler : IRequestHandler<UsernameAlreadyExistsQuery, bool>
	{
		public async Task<bool> Handle(UsernameAlreadyExistsQuery request, CancellationToken cancellationToken)
		{
			User user = await request.UserManager.FindByNameAsync(request.Username);
			return user != null;
		}
	}
}
