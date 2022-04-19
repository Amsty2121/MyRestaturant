using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Dishes.Commands.InsertDish;
using Common.Dto.Dishes;
using Common.Dto.Orders;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Orders.Commands.InsertOrder
{
    public class InsertOrderCommand : IRequest<Order>
    {
        public InsertOrderDto Dto { get; set; }
    }

    public class InsertOrderCommandHandler : IRequestHandler<InsertOrderCommand, Order>
    {
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<DishOrder> _dishOrderRepository;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderStatus> _orderStatusRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        public InsertOrderCommandHandler(IGenericRepository<Dish> dishRepository,
                                         IGenericRepository<DishOrder> dishOrderRepository,
                                         IGenericRepository<Order> orderRepository,
                                         IGenericRepository<OrderStatus> orderStatusRepository,
                                         IGenericRepository<Table> tableRepository)
        {
            _dishRepository = dishRepository;
            _dishOrderRepository = dishOrderRepository;
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _tableRepository = tableRepository;
        }

        public async Task<Order> Handle(InsertOrderCommand request, CancellationToken cancellationToken)
        {
            Dish dish = await _dishRepository.FirstOrDefault(x => x.Id == request.Dto.DishId);
            if (dish == null)
            {
                throw new EntityAlreadyExistsException("This Dish not exists");
            }

            OrderStatus orderStatus = await _orderStatusRepository.GetById(request.Dto.OrderStatusId);

            if (orderStatus == null)
            {
                throw new EntityDoesNotExistException("This OrderStatus does not exist");
            }

            Table table = await _tableRepository.GetById(request.Dto.TableId);

            if (table == null)
            {
                throw new EntityDoesNotExistException("This Table does not exist");
            }

            var order = new Order()
            {
                OrderNrPortions = request.Dto.OrderNrPortions,
                OrderDescription = request.Dto.OrderDescription,
                TableId = request.Dto.TableId,
                OrderStatusId = request.Dto.OrderStatusId
            };

            await _orderRepository.Add(order);

            var dishOrder = new DishOrder()
            {
                DishId = request.Dto.DishId,
                OrderId = order.Id
            };
            await _dishOrderRepository.Add(dishOrder);

            order.DishOrder = dishOrder;
            await _orderRepository.Update(order);
            return order;
        }
    }
}