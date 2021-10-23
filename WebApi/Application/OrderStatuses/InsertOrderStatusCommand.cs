using Application.Common.Interfaces;
using Common.Dto.OrderStatuses;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderStatuses
{
    public class InsertOrderStatusCommand : IRequest<OrderStatus>
    {
        public InsertOrderStatusDto Dto { get; set; }
    }

    public class InsertOrderStatusCommandHandler : IRequestHandler<InsertOrderStatusCommand, OrderStatus>
    {
        private readonly IGenericRepository<OrderStatus> _orderStatusRepository;

        public InsertOrderStatusCommandHandler(IGenericRepository<OrderStatus> orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
        }

        public async Task<OrderStatus> Handle(InsertOrderStatusCommand request, CancellationToken cancellationToken)
        {
            OrderStatus orderStatus = await _orderStatusRepository.FirstOrDefault(x => x.OrderStatusName == request.Dto.OrderStatusName);
            if (orderStatus != null)
            {
                throw new EntityAlreadyExistsException("This OrderStatus already exists");
            }

            orderStatus = new OrderStatus()
            {
                OrderStatusName = request.Dto.OrderStatusName
            };

            await _orderStatusRepository.Add(orderStatus);

            return orderStatus;
        }
    }
}