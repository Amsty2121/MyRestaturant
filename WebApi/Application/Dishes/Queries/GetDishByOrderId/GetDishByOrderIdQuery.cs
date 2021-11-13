using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Dishes.Queries.GetDishByOrderId
{
    public class GetDishByOrderIdQuery : IRequest<Dish>
    {
        public int OrderId { get; set; }
    }

    public class GetDishByIdQueryHandler : IRequestHandler<GetDishByOrderIdQuery, Dish>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Dish> _dishRepository;

        public GetDishByIdQueryHandler(IGenericRepository<Order> orderRepository,
            IGenericRepository<Dish> dishRepository)
        {
            _orderRepository = orderRepository;
            _dishRepository = dishRepository;
        }

        public async Task<Dish> Handle(GetDishByOrderIdQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdWithInclude(request.OrderId, x => x.Dish);
            
            return await _dishRepository.GetById(order.DishId);
        }
    }
}