using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Waiters;

namespace Application.Waiters.Queries.GetWaiterByOrderId
{
    public class GetWaiterByOrderIdQuery : IRequest<GetWaiterDto>
    {
        public int OrderId { get; set; }
    }

    public class GetWaiterByIdQueryHandler : IRequestHandler<GetWaiterByOrderIdQuery, GetWaiterDto>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;

        public GetWaiterByIdQueryHandler(IGenericRepository<Order> orderRepository,
                                         IGenericRepository<Table> tableRepository,
                                         IGenericRepository<Waiter> waiterRepository)
        {
            _orderRepository = orderRepository;
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
        }

        public async Task<GetWaiterDto> Handle(GetWaiterByOrderIdQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdWithInclude(request.OrderId, x => x.Table);
            var table = await _tableRepository.GetByIdWithInclude(order.TableId, x => x.Waiter);
            var waiter = await _waiterRepository.GetByIdWithInclude(table.WaiterId, x => x.UserDetails);

            var getWaiterDto = new GetWaiterDto()
            {
                Id = waiter.Id,
                FirstName = waiter.UserDetails.FirstName,
                LastName = waiter.UserDetails.LastName
            };
            return getWaiterDto;
        }
    }
}