using System.Collections.Generic;
using Application.Common.Interfaces;
using Common.Models.PagedRequest;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Kitcheners.Queries.GetKitchenerByOrderId;
using Common.Dto.Kitcheners;
using Common.Dto.Waiters;


namespace Application.Orders.Queries.GetOrdersPaged
{
   public class GetOrderPagedDto
    {
        public int Id { get; set; }
        public int OrderNrPortions { get; set; }
        public string OrderDescription { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Table Table { get; set; }
        public GetWaiterDto Waiter { get; set; }
        public Dish Dish { get; set; }
        public GetKitchenerDto Kitchener { get; set; }
    }


    public class GetOrderPagedQuery : IRequest<PaginatedResult<Order>>
    {
        public PagedRequest PagedRequest { get; set; }
    }

    public class GetOrdersPagedQueryHandler : IRequestHandler<GetOrderPagedQuery, PaginatedResult<Order>>
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public GetOrdersPagedQueryHandler(IGenericRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<PaginatedResult<Order>> Handle(GetOrderPagedQuery request, CancellationToken cancellationToken)
        {
            var a = await _orderRepository.GetPagedData<Order>(request.PagedRequest);
            return a;
        }
    }
}