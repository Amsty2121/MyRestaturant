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
    public class AssignedKitchenerToDishOrder
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
        public int KitchenerId { get; set; }
        public string KitchenerName { get; set; }


    }
    public class AssignKitchenerToDIshOrderCommand : IRequest<AssignedKitchenerToDishOrder>
    {
        public int KitchenerId { get; set; }
        public AssignKitchenerToDIshOrderDto Dto { get; set; }
    }

    public class AssignKitchenerToDIshOrderCommandHandler : IRequestHandler<AssignKitchenerToDIshOrderCommand, AssignedKitchenerToDishOrder>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IOrderUnitOfWork _orderUnitOfWork;
        private readonly IGenericRepository<OrderStatus> _orderStatusRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;
        private readonly IGenericRepository<Kitchener> _kitchenerRepository;

        public AssignKitchenerToDIshOrderCommandHandler(IGenericRepository<Dish> dishRepository,
                                         IGenericRepository<Order> orderRepository,
                                         IOrderUnitOfWork orderUnitOfWork,
                                         IGenericRepository<OrderStatus> orderStatusRepository,
                                         IGenericRepository<Table> tableRepository,
                                         IGenericRepository<Waiter> waiterRepository,
                                         IGenericRepository<Kitchener> kitchenerRepository)
        {
            _orderUnitOfWork = orderUnitOfWork;
            _dishRepository = dishRepository;
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
            _kitchenerRepository = kitchenerRepository;
        }

        public async Task<AssignedKitchenerToDishOrder> Handle(AssignKitchenerToDIshOrderCommand request, CancellationToken cancellationToken)
        {
            Kitchener kitchener = await _kitchenerRepository.GetByIdWithInclude(request.KitchenerId,x=>x.UserDetails);
            if (kitchener == null)
            {
                throw new EntityDoesNotExistException("The kitchener does not exist");
            }

            Order updatedOrder = await _orderRepository.GetByIdWithInclude(request.Dto.OrderId, x => x.DishOrder,x=>x.OrderStatus,x=>x.Table);
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

            DishOrder dishOrder = (await _orderUnitOfWork.DishOrderRepository.GetWhere(x => x.OrderId == updatedOrder.Id)).FirstOrDefault();

            Dish dish = await _dishRepository.GetById(dishOrder.DishId);

            Waiter waiter = await _waiterRepository.GetByIdWithInclude(table.WaiterId, x => x.UserDetails);

            dishOrder.KitchenerId = kitchener.Id;
            dishOrder.Kitchener = kitchener;

            updatedOrder.DishOrder = dishOrder;

            await _orderUnitOfWork.DishOrderRepository.Update(dishOrder);
            await _orderRepository.Update(updatedOrder);

            var assignedKitchenerToDishOrder = new AssignedKitchenerToDishOrder()
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
                KitchenerId = kitchener.Id,
                KitchenerName = kitchener.UserDetails.FirstName + " " + kitchener.UserDetails.LastName,

            };

            return assignedKitchenerToDishOrder;
        }
    }
}
