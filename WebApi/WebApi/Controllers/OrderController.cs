using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Dishes.Queries.GetDishByOrderId;
using Application.Kitcheners.Queries.GetKitchenerByOrderId;
using Application.Orders.Commands.DeleteOrder;
using Application.Orders.Commands.InsertOrder;
using Application.Orders.Commands.UpdateOrder;
using Application.Orders.Queries.GetOrderById;
using Application.Orders.Queries.GetOrdersList;
using Application.Orders.Queries.GetOrdersPaged;
using Application.OrderStatuses.Queries.GetStatusByOrderId;
using Application.Tables.Queries.GetTableByOrderId;
using Application.Waiters.Queries.GetWaiterByOrderId;
using Common.Dto.Orders;
using Common.Models.PagedRequest;
using Domain.Entities;

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

        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {

            var orders = await _mediator.Send(new GetOrdersListQuery());
            var results = orders.Select(x => _mapper.Map<OrdersWithStatusesTablesAndWaiters>(x));

            return Ok(results);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var queryOrder = new GetOrderByIdQuery() { OrderId = orderId };
            OrderWithStatusTableAndWaiter orderWithStatus = await _mediator.Send(queryOrder);

            var result = _mapper.Map<GetOrderDto>(orderWithStatus);

            return Ok(result);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("paginated-search")]
        public async Task<IActionResult> GetOrdersPaged([FromBody] PagedRequest pagedRequest)
        {
            var query = new GetOrderPagedQuery() { PagedRequest= pagedRequest };
            var orders = await _mediator.Send(query);
            var ordersResult = _mapper.Map<PaginatedResult<GetOrderPagedDto>>(orders);

            foreach (var order in ordersResult.Items)
            {
                order.OrderStatus = (await _mediator.Send(new GetStatusByOrderIdQuery() { OrderId = order.Id }));
                order.Table = (await _mediator.Send(new GetTableByOrderIdQuery() { OrderId = order.Id }));
                order.Kitchener = (await _mediator.Send(new GetKitchenerByOrderIdQuery() { OrderId = order.Id }));
                order.Waiter = (await _mediator.Send(new GetWaiterByOrderIdQuery() { OrderId = order.Id }));
                order.Dish = (await _mediator.Send(new GetDishByOrderIdQuery() { OrderId = order.Id }));
            }

            var a = ordersResult;
            return Ok(a);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrder(InsertOrderDto dto)
        {
            InsertOrderCommand command = new InsertOrderCommand() { Dto = dto };
            var order = await _mediator.Send(command);
            var result = _mapper.Map<InsertedOrderDto>(order);
            return CreatedAtAction(nameof(GetOrder), new { id = result.Id }, result);
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
        }
    }
}