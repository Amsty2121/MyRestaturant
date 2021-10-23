using Application.Users.Queries.UsernameAlreadyExists;
using Common.Dto.User;
using Domain.Entities.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : BaseController
	{
		private readonly IMediator _mediator;
		private readonly UserManager<User> _userManager;

		public UserController(IMediator mediator, UserManager<User> userManager)
		{
			_mediator = mediator;
			_userManager = userManager;
		}

		[HttpPost]
		public async Task<IActionResult> CheckIfUsernameExists([FromBody] UserDataDto dto)
		{
			var query = new UsernameAlreadyExistsQuery() { Username = dto.Username, UserManager = _userManager };
			var usernameExists = await _mediator.Send(query);

			return Ok(usernameExists);
		}
	}
}
