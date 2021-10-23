using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Application.Waiters.Commands.RegisterWaiter;
using Application.Waiters.Queries.GetWaiterById;
using Application.Waiters.Queries.GetWaitersList;
using Common.Dto.Waiters;
using Domain.Entities.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaiterController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public WaiterController(IMediator mediator, IMapper mapper, UserManager<User> userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetWaiters()
        {
            var waiters = await _mediator.Send(new GetWaitersListQuery());

            var result = waiters.Select(x => _mapper.Map<GetWaiterListDto>(x));

            return Ok(result);
        }

        [HttpGet("{waiterId}")]
        public async Task<IActionResult> GetWaiterById(int waiterId)
        {
            GetWaiterByIdQuery query = new GetWaiterByIdQuery() { WaiterId = waiterId };

            WaiterWithTablesAndOrders waiter = await _mediator.Send(query);
            var result = _mapper.Map<GetWaiterDto>(waiter);

            return Ok(result);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterWaiter(InsertWaiterDto dto)
        {
            var command = new RegisterWaiterCommand() { Dto = dto, UserManager = _userManager };
            var registerResult = await _mediator.Send(command);

            if (registerResult.Succeeded)
            {
                return Ok(registerResult);
            }

            return BadRequest();
        }
    }
}