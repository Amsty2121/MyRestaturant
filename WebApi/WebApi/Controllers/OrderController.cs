using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Orders.Commands.DeleteOrder;
using Application.Orders.Commands.InsertOrder;
using Application.Orders.Commands.UpdateOrder;
using Application.Orders.Queries.GetOrderById;
using Application.Orders.Queries.GetOrdersList;
using Common.Dto.Orders;
using Domain.Entities;
using OrdersWithStatusesTablesAndWaiters = Application.Orders.Queries.GetOrdersList.OrdersWithStatusesTablesAndWaiters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /*[HttpGet]
        public async Task<IActionResult> GetOrders()
        {

            var orders = await _mediator.Send(new GetOrdersListQuery());
            var results = orders.Select(x => _mapper.Map<OrdersWithStatusesTablesAndWaiters>(x));

            return Ok(results);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var queryOrder = new GetOrderByIdQuery() { OrderId = orderId };
            var orderWithStatus = await _mediator.Send(queryOrder);

            var result = _mapper.Map<GetOrderDto>(orderWithStatus);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrder(InsertOrderDto dto)
        {
            InsertOrderCommand command = new InsertOrderCommand() { Dto = dto };
            var order = await _mediator.Send(command);
            var result = _mapper.Map<InsertedOrderDto>(order);
            return CreatedAtAction(nameof(GetOrders), new { id = result.Id }, result);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            DeleteOrderCommand command = new DeleteOrderCommand() { OrderId = orderId };
            var isDeleted = await _mediator.Send(command);

            return isDeleted ? NoContent() : NotFound();
        }

        [HttpPatch("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderDto dto)
        {

            UpdateOrderCommand command = new UpdateOrderCommand()
            {
                Id = orderId,
                Dto = dto
            };
            var order = await _mediator.Send(command);

            var result = _mapper.Map<UpdatedOrderDto>(order);

            return Ok(result);
        }*/
    }
}