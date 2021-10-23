using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderStatuses.Queries.GetOrderStatusById
{
    public class GetOrderStatusByIdQuery : IRequest<OrderStatus>
    {
        public int OrderStatusId { get; set; }
    }

    public class GetOrderStatusByIdQueryHandler : IRequestHandler<GetOrderStatusByIdQuery, OrderStatus>
    {
        private readonly IGenericRepository<OrderStatus> _orderStatusRepository;

        public GetOrderStatusByIdQueryHandler(IGenericRepository<OrderStatus> orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
        }

        public async Task<OrderStatus> Handle(GetOrderStatusByIdQuery request, CancellationToken cancellationToken)
        {
            OrderStatus orderStatus = await _orderStatusRepository.GetByIdWithInclude(request.OrderStatusId, x => x.Orders);

            if (orderStatus == null)
            {
                throw new EntityDoesNotExistException("The OrderStatus does not exist");
            }

            return orderStatus;
        }
    }
}