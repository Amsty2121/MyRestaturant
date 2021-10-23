using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public DeleteOrderCommandHandler(IGenericRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            Order deletedOrder = await _orderRepository.FirstOrDefault(x => x.Id == request.OrderId);

            if (deletedOrder != null)
            {
                await _orderRepository.Remove(deletedOrder);
                return true;
            }

            return false;
        }
    }
}