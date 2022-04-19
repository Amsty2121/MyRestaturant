using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Ingredients.Queries.GetIngredientsList;

namespace Application.Orders.Queries.GetOrdersList
{
    public class OrdersWithStatusesTablesAndWaiters
    {
        public int Id { get; set; }
        public int OrderNrPortions { get; set; }
        public string OrderDescription { get; set; }
        public int WaiterId { get; set; }
        public string WaiterName { get; set; }
        public int TableId { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
        public int DishId { get; set; }
        public string DishName { get; set; }
    }
    public class GetOrdersListQuery : IRequest<IEnumerable<OrdersWithStatusesTablesAndWaiters>>
    {
    }

    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, IEnumerable<OrdersWithStatusesTablesAndWaiters>>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<DishOrder> _dishOrderRepository;
        private readonly IGenericRepository<OrderStatus> _orderStatusRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;
        private readonly IGenericRepository<Dish> _dishRepository;

        public GetOrdersListQueryHandler(IGenericRepository<Order> orderRepository,
            IGenericRepository<DishOrder> dishOrderRepository,
            IGenericRepository<OrderStatus> orderStatusRepository,
            IGenericRepository<Table> tableRepository,
            IGenericRepository<Waiter> waiterRepository,
            IGenericRepository<Dish> dishRepository)
        {
            _orderRepository = orderRepository;
            _dishOrderRepository = dishOrderRepository;
            _orderStatusRepository = orderStatusRepository;
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
            _dishRepository = dishRepository;
        }

        public async Task<IEnumerable<OrdersWithStatusesTablesAndWaiters>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Order> orders = await _orderRepository.GetAll();

            var ordersWithStatusesTablesAndWaiters = new List<OrdersWithStatusesTablesAndWaiters>();

            foreach (var order in orders)
            {
                var dishOrders = (await _dishOrderRepository.GetWhere(x => x.OrderId == order.Id)).ToList();

                var orderStatus = await _orderStatusRepository.GetById(order.OrderStatusId);
                var table = await _tableRepository.GetById(order.TableId);
                var waiter = await _waiterRepository.GetByIdWithInclude(order.TableId, x => x.UserDetails);

                var dish = await _dishRepository.GetById(dishOrders[0].DishId);


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
                    DishName = dish.DishName
                });
            }

            return ordersWithStatusesTablesAndWaiters;
        }
    }
}