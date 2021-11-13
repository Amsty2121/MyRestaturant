using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Orders;

namespace Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<OrderUpdating>
    {
        public int Id { get; set; }
        public UpdateOrderDto Dto { get; set; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderUpdating>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<OrderStatus> _orderStatusRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;
        private readonly IGenericRepository<Kitchener> _kitchenerRepository;

        public UpdateOrderCommandHandler(IGenericRepository<Dish> dishRepository,
                                         IGenericRepository<Order> orderRepository,
                                         IGenericRepository<OrderStatus> orderStatusRepository,
                                         IGenericRepository<Table> tableRepository,
                                         IGenericRepository<Waiter> waiterRepository,
                                         IGenericRepository<Kitchener> kitchenerRepository)
        {
            _dishRepository = dishRepository;
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
            _kitchenerRepository = kitchenerRepository;
        }

        public async Task<OrderUpdating> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order updatedOrder = await _orderRepository.GetById(request.Id);
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
                throw new EntityDoesNotExistException("The Dish does not exist");
            }
            updatedOrder.DishId= dish.Id;
            updatedOrder.Dish = dish;

            Waiter waiter = await _waiterRepository.GetByIdWithInclude(table.WaiterId,x=>x.UserDetails);
            if (waiter == null)
            {
                throw new EntityDoesNotExistException("The Waiter does not exist");
            }

            updatedOrder.OrderDescription = request.Dto.OrderDescription;
            updatedOrder.OrderNrPortions = request.Dto.OrderNrPortions;


            string kitchenerName = "not assigned";
            if (request.Dto.KitchenerId != null)
            {
                Kitchener kitchener = await _kitchenerRepository.GetByIdWithInclude(request.Dto.KitchenerId.Value, x => x.UserDetails);
                if (kitchener == null)
                {
                    throw new EntityDoesNotExistException("The Kitchener does not exist");
                }

                kitchenerName = kitchener.UserDetails.FirstName + " " + kitchener.UserDetails.LastName;
                updatedOrder.KitchenerId = kitchener.Id;
                updatedOrder.Kitchener = kitchener;
            }
            else
            {
                updatedOrder.KitchenerId = null;
                updatedOrder.Kitchener = null;
            }
            
            
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
                DishId = updatedOrder.Dish.Id,
                DishName = updatedOrder.Dish.DishName,
                KitchenerId = request.Dto.KitchenerId,
                KitchenerName = kitchenerName
                

            };

            return orderUpdating;
        }
    }
}
