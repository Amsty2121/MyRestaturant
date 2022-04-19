using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Common.Dto.Kitcheners;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Kitcheners.Commands.AssignKitchenerToDIshOrder
{
    public class AssignKitchenerToDIshOrderCommand : IRequest<AssignedKitchenerToDIshOrderDto>
    {
        public int OrderId { get; set; }
        public AssignKitchenerToDIshOrderDto Dto { get; set; }
    }

    public class AssignKitchenerToDIshOrderCommandHandler : IRequestHandler<AssignKitchenerToDIshOrderCommand, AssignedKitchenerToDIshOrderDto>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<DishOrder> _dishOrderRepository;
        private readonly IGenericRepository<OrderStatus> _orderStatusRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;
        private readonly IGenericRepository<Kitchener> _kitchenerRepository;

        public AssignKitchenerToDIshOrderCommandHandler(IGenericRepository<Dish> dishRepository,
                                         IGenericRepository<DishOrder> dishOrderRepository,
                                         IGenericRepository<Order> orderRepository,
                                         IGenericRepository<OrderStatus> orderStatusRepository,
                                         IGenericRepository<Table> tableRepository,
                                         IGenericRepository<Waiter> waiterRepository,
                                         IGenericRepository<Kitchener> kitchenerRepository)
        {
            _dishRepository = dishRepository;
            _dishOrderRepository = dishOrderRepository;
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
            _kitchenerRepository = kitchenerRepository;
        }

        public async Task<AssignedKitchenerToDIshOrderDto> Handle(AssignKitchenerToDIshOrderCommand request, CancellationToken cancellationToken)
        {
            Kitchener kitchener = await _kitchenerRepository.GetByIdWithInclude(request.Dto.KitchenerId,x=>x.UserDetails);
            if (kitchener == null)
            {
                throw new EntityDoesNotExistException("The kitchener does not exist");
            }

            Order updatedOrder = await _orderRepository.GetByIdWithInclude(request.OrderId,x=>x.DishOrder,x=>x.OrderStatus,x=>x.Table);
            if (updatedOrder == null)
            {
                throw new EntityDoesNotExistException("The Order does not exist");
            }

            Table table = await _tableRepository.GetById(updatedOrder.TableId);

            updatedOrder.TableId = table.Id;
            updatedOrder.Table = table;

            OrderStatus orderStatus = await _orderStatusRepository.GetById(updatedOrder.OrderStatusId);

            updatedOrder.OrderStatusId = orderStatus.Id;
            updatedOrder.OrderStatus = orderStatus;

            Dish dish = await _dishRepository.GetById(updatedOrder.DishOrder.DishId);

            DishOrder dishOrder =
                (await _dishOrderRepository.GetWhere(x => x.OrderId == updatedOrder.Id && x.DishId == dish.Id)).Single();

            updatedOrder.DishOrder = dishOrder;

            Waiter waiter = await _waiterRepository.GetByIdWithInclude(table.WaiterId, x => x.UserDetails);

            updatedOrder.KitchenerId = kitchener.Id;
            updatedOrder.Kitchener = kitchener;

            await _orderRepository.Update(updatedOrder);

            var assignedKitchenerToDishOrder = new AssignedKitchenerToDIshOrderDto()
            {
                Id = updatedOrder.Id,
                OrderNrPortions = updatedOrder.OrderNrPortions,
                OrderDescription = updatedOrder.OrderDescription,
                WaiterId = waiter.Id,
                WaiterName = waiter.UserDetails.FirstName + " " + waiter.UserDetails.LastName,
                TableId = updatedOrder.TableId,
                OrderStatusId = updatedOrder.OrderStatusId,
                OrderStatusName = updatedOrder.OrderStatus.OrderStatusName,
                DishId = dish.Id,
                DishName = dish.DishName,
                KitchenerId = kitchener.Id,
                KitchenerName = kitchener.UserDetails.FirstName + " " + kitchener.UserDetails.LastName,
            };

            return assignedKitchenerToDishOrder;
        }
    }
}
