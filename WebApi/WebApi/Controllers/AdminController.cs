using Application.Admins.Commands.RegisterAdmin;
using Common.Dto.Admins;
using Domain.Entities.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public AdminController(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(InsertAdminDto dto)
        {
            var command = new RegisterAdminCommand() { Dto = dto, UserManager = _userManager };
            var registerResult = await _mediator.Send(command);

            if (registerResult.Succeeded)
            {
                return Ok(registerResult);
            }

            return BadRequest();
        }
    }
}