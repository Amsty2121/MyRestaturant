using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Application.Kitcheners.Commands.AssignKitchenerToDIshOrder;
using Application.Kitcheners.Commands.RegisterKitchener;
using Application.Kitcheners.Queries.GetKitchenerById;
using Application.Kitcheners.Queries.GetKitchenersList;
using Common.Dto.Kitcheners;
using Domain.Entities.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitchenerController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public KitchenerController(IMediator mediator, IMapper mapper, UserManager<User> userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetKitcheners()
        {
            var kitcheners = await _mediator.Send(new GetKitchenersListQuery());

            var result = kitcheners.Select(x => _mapper.Map<GetKitchenerListDto>(x));

            return Ok(result);
        }

        [HttpGet("{kitchenerId}")]
        public async Task<IActionResult> GetKitchenerById(int kitchenerId)
        {
            GetKitchenerByIdQuery query = new GetKitchenerByIdQuery() { KitchenerId = kitchenerId };

            KitchenerWithOrders kitchener = await _mediator.Send(query);
            var result = _mapper.Map<GetKitchenerDto>(kitchener);

            return Ok(result);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterKitchener(InsertKitchenerDto dto)
        {
            var command = new RegisterKitchenerCommand() { Dto = dto, UserManager = _userManager };
            var registerResult = await _mediator.Send(command);

            if (registerResult.Succeeded)
            {
                return Ok(registerResult);
            }

            return BadRequest();
        }

        [HttpPatch("{orderId}")]
        public async Task<IActionResult> AssignKitchenerToOrder(int orderId, AssignKitchenerToDIshOrderDto dto)
        {
            AssignKitchenerToDIshOrderCommand command = new AssignKitchenerToDIshOrderCommand()
            {
                OrderId = orderId,
                Dto = dto
            };
            var order = await _mediator.Send(command);

            var result = _mapper.Map<AssignedKitchenerToDIshOrderDto>(order);

            return Ok(result);
        }
    }
}