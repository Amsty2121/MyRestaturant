using System.Collections.Generic;
using Application.Common.Interfaces;
using Common.Dto.Dishes;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Application.Dishes.Queries.GetDishById;
using Common.Dto.Orders;

namespace Application.Orders.Queries.GetOrderById
{
    
    public class GetOrderByIdQuery : IRequest<OrderWithStatusTableAndWaiter>
    {
        public int OrderId { get; set; }
    }

    class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderWithStatusTableAndWaiter>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderStatus> _orderStatusRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;
        private readonly IGenericRepository<Kitchener> _kitchenerRepository;
        private readonly IGenericRepository<Dish> _dishRepository;

        public GetOrderByIdQueryHandler(IGenericRepository<Order> orderRepository,
            IGenericRepository<OrderStatus> orderStatusRepository,
            IGenericRepository<Table> tableRepository,
            IGenericRepository<Waiter> waiterRepository,
            IGenericRepository<Kitchener> kitchenerRepository,
            IGenericRepository<Dish> dishRepository)
        {
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
            _kitchenerRepository = kitchenerRepository;
            _dishRepository = dishRepository;
        }

        public async Task<OrderWithStatusTableAndWaiter> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            Order order = await _orderRepository.GetByIdWithInclude(request.OrderId);

            if (order == null)
            {
                throw new EntityDoesNotExistException("The Order does not exist");
            }


            var orderStatus = await _orderStatusRepository.GetById(order.OrderStatusId);
            var table = await _tableRepository.GetById(order.TableId);
            var waiter = await _waiterRepository.GetByIdWithInclude(order.TableId, x => x.UserDetails);
            
            
            string kitchenerName;
            Kitchener kitchener;
            if (order.KitchenerId != null)
            {

                kitchener = await _kitchenerRepository.GetByIdWithInclude(order.KitchenerId.Value, x => x.UserDetails);
                kitchenerName = kitchener.UserDetails.FirstName + " " + kitchener.UserDetails.LastName;
            }
            else
            {
                order.KitchenerId = null;
                kitchenerName = null;
            }


            var dish = await _dishRepository.GetById(order.DishId);



            var orderWithStatusTableAndWaiter = new OrderWithStatusTableAndWaiter()
            {
                Id = order.Id,
                OrderNrPortions = order.OrderNrPortions,
                OrderDescription = order.OrderDescription,
                WaiterId = waiter.Id,
                WaiterName  = waiter.UserDetails.FirstName + " " + waiter.UserDetails.LastName,
                TableId = table.Id,
                OrderStatusId = orderStatus.Id,
                OrderStatusName = orderStatus.OrderStatusName,
                DishId = dish.Id,
                DishName = dish.DishName,
                KitchenerId = order.KitchenerId,
                KitchenerName = kitchenerName,
            };

            return orderWithStatusTableAndWaiter;
        }
    }
}