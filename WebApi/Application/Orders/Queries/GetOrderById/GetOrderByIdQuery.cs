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

namespace Application.Orders.Queries.GetOrderById
{
    public class OrderWithStatusTableAndWaiter
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
    public class GetOrderByIdQuery : IRequest<OrderWithStatusTableAndWaiter>
    {
        public int OrderId { get; set; }
    }

    class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderWithStatusTableAndWaiter>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<DishOrder> _dishOrderRepository;
        private readonly IGenericRepository<OrderStatus> _orderStatusRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;
        private readonly IGenericRepository<Dish> _dishRepository;

        public GetOrderByIdQueryHandler(IGenericRepository<Order> orderRepository,
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

        public async Task<OrderWithStatusTableAndWaiter> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            Order order = await _orderRepository.GetByIdWithInclude(request.OrderId, x => x.DishOrder);

            if (order == null)
            {
                throw new EntityDoesNotExistException("The Order does not exist");
            }

            var dishOrders = (await _dishOrderRepository.GetWhere(x => x.OrderId == request.OrderId)).ToList();

            var orderStatus = await _orderStatusRepository.GetById(order.OrderStatusId);
            var table = await _tableRepository.GetById(order.TableId);
            var waiter = await _waiterRepository.GetByIdWithInclude(order.TableId, x => x.UserDetails);

            var dish = await _dishRepository.GetById(dishOrders[0].DishId);



            var orderWithStatusTableAndWaiter = new OrderWithStatusTableAndWaiter()
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
            };

            return orderWithStatusTableAndWaiter;
        }
    }
}