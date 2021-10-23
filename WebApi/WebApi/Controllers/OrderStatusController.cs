using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.OrderStatuses;
using Application.OrderStatuses.Queries.GetOrderStatusById;
using Application.OrderStatuses.Queries.GetOrderStatusesList;
using Common.Dto.OrderStatuses;
using Domain.Entities; 

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderStatusController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderStatus()
        {
            var orderStatus = await _mediator.Send(new GetOrderStatusesListQuery());
            var result = orderStatus.Select(x => _mapper.Map<GetOrderStatusesListDto>(x));
            return Ok(result);
        }

        [HttpGet("{orderStatusId}")]
        public async Task<IActionResult> GetOrderStatusById(int orderStatusId)
        {
            var query = new GetOrderStatusByIdQuery() { OrderStatusId = orderStatusId };
            OrderStatus orderStatus = await _mediator.Send(query);
            var result = _mapper.Map<GetOrderStatusDto>(orderStatus);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrderStatus(InsertOrderStatusDto dto)
        {
            InsertOrderStatusCommand command = new InsertOrderStatusCommand() { Dto = dto };
            var orderStatus = await _mediator.Send(command);
            var result = _mapper.Map<InsertedOrderStatusDto>(orderStatus);
            return CreatedAtAction(nameof(GetOrderStatus), new { id = result.Id }, result);
        }
    }
}