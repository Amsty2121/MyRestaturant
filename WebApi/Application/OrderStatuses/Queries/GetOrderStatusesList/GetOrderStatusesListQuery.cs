using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderStatuses.Queries.GetOrderStatusesList
{
    public class GetOrderStatusesListQuery : IRequest<IEnumerable<OrderStatus>>
    {
    }

    public class GetOrderStatusesListHandler : IRequestHandler<GetOrderStatusesListQuery, IEnumerable<OrderStatus>>
    {
        private readonly IGenericRepository<OrderStatus> _orderStatusesRepository;

        public GetOrderStatusesListHandler(IGenericRepository<OrderStatus> orderStatusesRepository)
        {
            _orderStatusesRepository = orderStatusesRepository;
        }
        public async Task<IEnumerable<OrderStatus>> Handle(GetOrderStatusesListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<OrderStatus> orderStatuses = await _orderStatusesRepository.GetAll();

            return orderStatuses;
        }
    }
}