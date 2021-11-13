using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tables.Queries.GetTableByOrderId
{
    public class GetTableByOrderIdQuery : IRequest<Table>
    {
        public int OrderId { get; set; }
    }

    public class GetTableByIdQueryHandler : IRequestHandler<GetTableByOrderIdQuery, Table>
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public GetTableByIdQueryHandler(IGenericRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Table> Handle(GetTableByOrderIdQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdWithInclude(request.OrderId, x => x.Table);

            return order.Table;
        }
    }
}