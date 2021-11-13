using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderStatuses.Queries.GetStatusByOrderId
{
    public class GetStatusByOrderIdQuery : IRequest<OrderStatus>
    {
        public int OrderId { get; set; }
    }

    public class GetOrderStatusByIdQueryHandler : IRequestHandler<GetStatusByOrderIdQuery, OrderStatus>
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public GetOrderStatusByIdQueryHandler( IGenericRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderStatus> Handle(GetStatusByOrderIdQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdWithInclude(request.OrderId, x => x.OrderStatus);

            return order.OrderStatus;
        }
    }
}