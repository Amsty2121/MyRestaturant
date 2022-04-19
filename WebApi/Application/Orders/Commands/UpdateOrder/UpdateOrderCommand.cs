using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Dishes.Queries.GetDishesList;
using Common.Dto.Orders;

namespace Application.Orders.Commands.UpdateOrder
{
    public class OrderUpdating
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
    public class UpdateOrderCommand : IRequest<OrderUpdating>
    {
        public int Id { get; set; }
        public UpdateOrderDto Dto { get; set; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderUpdating>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IOrderUnitOfWork _orderUnitOfWork;
        private readonly IGenericRepository<OrderStatus> _orderStatusRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;

        public UpdateOrderCommandHandler(IGenericRepository<Dish> dishRepository,
                                         IGenericRepository<Order> orderRepository,
                                         IOrderUnitOfWork orderUnitOfWork,
                                         IGenericRepository<OrderStatus> orderStatusRepository,
                                         IGenericRepository<Table> tableRepository,
                                         IGenericRepository<Waiter> waiterRepository)
        {
            _orderUnitOfWork = orderUnitOfWork;
            _dishRepository = dishRepository;
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
        }

        public async Task<OrderUpdating> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order updatedOrder = await _orderRepository.GetByIdWithInclude(request.Id, x => x.DishOrder);
            if (updatedOrder == null)
            {
                throw new EntityDoesNotExistException("The Order does not exist");
            }

            Table table = await _tableRepository.GetById(request.Dto.TableId);
            if (table == null)
            {
                throw new EntityDoesNotExistException("The Table does not exist");
            }

            updatedOrder.TableId = table.Id;
            updatedOrder.Table = table;

            OrderStatus orderStatus = await _orderStatusRepository.GetById(request.Dto.OrderStatusId);
            if (orderStatus == null)
            {
                throw new EntityDoesNotExistException("The OrderStatus does not exist");
            }

            updatedOrder.OrderStatusId = orderStatus.Id;
            updatedOrder.OrderStatus = orderStatus;

            Dish dish = await _dishRepository.GetById(request.Dto.DishId);
            if (dish == null)
            {
                throw new EntityDoesNotExistException("The OrderStatus does not exist");
            }

            Waiter waiter = await _waiterRepository.GetByIdWithInclude(table.WaiterId, x => x.UserDetails);

            DishOrder dishOrder = (await _orderUnitOfWork.DishOrderRepository.GetWhere(x => x.OrderId == request.Id)).FirstOrDefault();



            dishOrder.DishId = dish.Id;
            dishOrder.OrderId = request.Id;

            updatedOrder.OrderDescription = request.Dto.OrderDescription;
            updatedOrder.OrderNrPortions = request.Dto.OrderNrPortions;
            updatedOrder.DishOrder = dishOrder;

            await _orderUnitOfWork.DishOrderRepository.Update(dishOrder);
            await _orderRepository.Update(updatedOrder);

            var orderUpdating = new OrderUpdating()
            {
                Id = updatedOrder.Id,
                OrderNrPortions = updatedOrder.OrderNrPortions,
                OrderDescription = updatedOrder.OrderDescription,
                WaiterId = waiter.Id,
                WaiterName = waiter.UserDetails.FirstName + " " + waiter.UserDetails.LastName,
                TableId = updatedOrder.TableId,
                OrderStatusId = updatedOrder.OrderStatusId,
                OrderStatusName = updatedOrder.OrderStatus.OrderStatusName,
                DishId = updatedOrder.DishOrder.Dish.Id,
                DishName = updatedOrder.DishOrder.Dish.DishName,

            };

            return orderUpdating;
        }
    }
}
