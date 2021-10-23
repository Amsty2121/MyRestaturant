using Application.Roles.Queries.GetRolesList;
using AutoMapper;
using Common.Dto.Roles;
using Domain.Entities.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : BaseController
	{
		private readonly IMediator _mediator;
		private readonly RoleManager<Role> _roleManager;
		private readonly IMapper _mapper;

		public RoleController(IMediator mediator, RoleManager<Role> roleManager, IMapper mapper)
		{
			_mediator = mediator;
			_roleManager = roleManager;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetRolesQuery()
		{
			var query = new GetRolesListQuery() { RoleManager = _roleManager };
			var roles = await _mediator.Send(query);
			var result = roles.Select(x => _mapper.Map<GetRolesListDto>(x));

			return Ok(result);
		}
	}
}
