using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Ingredients.Queries.GetIngredientsList;
using Common.Dto.Orders;

namespace Application.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery : IRequest<IEnumerable<OrdersWithStatusesTablesAndWaiters>>
    {
    }

    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, IEnumerable<OrdersWithStatusesTablesAndWaiters>>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderStatus> _orderStatusRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;
        private readonly IGenericRepository<Kitchener> _kitchenerRepository;
        private readonly IGenericRepository<Dish> _dishRepository;

        public GetOrdersListQueryHandler(IGenericRepository<Order> orderRepository,
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

        public async Task<IEnumerable<OrdersWithStatusesTablesAndWaiters>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Order> orders = await _orderRepository.GetAll();

            var ordersWithStatusesTablesAndWaiters = new List<OrdersWithStatusesTablesAndWaiters>();

            foreach (var order in orders)
            {
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

                var orderStatus = await _orderStatusRepository.GetById(order.OrderStatusId);
                var table = await _tableRepository.GetById(order.TableId);
                var waiter = await _waiterRepository.GetByIdWithInclude(order.TableId, x => x.UserDetails);
                var dish = await _dishRepository.GetById(order.DishId);

                ordersWithStatusesTablesAndWaiters.Add(new OrdersWithStatusesTablesAndWaiters()
                {
                    Id = order.Id,
                    OrderNrPortions = order.OrderNrPortions,
                    OrderDescription = order.OrderDescription,
                    WaiterId = waiter.Id,
                    WaiterName = waiter.UserDetails.FirstName + " " + waiter.UserDetails.LastName,
                    TableId = table.Id,
                    OrderStatusId = orderStatus.Id,
                    OrderStatusName = orderStatus.OrderStatusName,
                    DishId = dish.Id,
                    DishName = dish.DishName,
                    KitchenerId = order.KitchenerId,
                    KitchenerName = kitchenerName,
                });
            }

            return ordersWithStatusesTablesAndWaiters;
        }
    }
}