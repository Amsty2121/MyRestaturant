using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Orders;

namespace Application.Orders.Queries.GetOrdersByTableId
{
    public class GetOrdersByTableIdQuery : IRequest<ICollection<GetOrderDto>>
    {
        public int TableId { get; set; }
    }

    public class GetOrdersByIdQueryHandler : IRequestHandler<GetOrdersByTableIdQuery, ICollection<GetOrderDto>>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<Table> _tableRepository;

        public GetOrdersByIdQueryHandler(IGenericRepository<Order> orderRepository,
            IGenericRepository<Dish> dishRepository,
            IGenericRepository<Table> tableRepository)
        {
            _orderRepository = orderRepository;
            _dishRepository = dishRepository;
            _tableRepository = tableRepository;
        }

        public async Task<ICollection<GetOrderDto>> Handle(GetOrdersByTableIdQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetWhere(x => x.TableId == request.TableId);

            ICollection<GetOrderDto> result = new List<GetOrderDto>();
            foreach (var order in orders)
            {
                var ord = await _orderRepository.GetByIdWithInclude(order.Id, 
                    x => x.Dish,
                    x=>x.Kitchener.UserDetails,
                    x=>x.OrderStatusId,
                    x=>x.Table,
                    x=>x.Table.Waiter.UserDetails);
                
                var mappedDish = new GetOrderDto()
                {
                    Id = ord.Id,
                    DishId = ord.DishId,
                    DishName = ord.Dish.DishName,
                    KitchenerId = ord.KitchenerId,
                    KitchenerName = ord.Kitchener.UserDetails.FirstName + " " + ord.Kitchener.UserDetails.LastName,
                    OrderDescription = ord.OrderDescription,
                    OrderNrPortions = ord.OrderNrPortions,
                    OrderStatusId = ord.OrderStatusId,
                    OrderStatusName = ord.OrderStatus.OrderStatusName,
                    TableId = ord.TableId,
                    WaiterId = ord.Table.WaiterId,
                    WaiterName = ord.Table.Waiter.UserDetails.FirstName + " " + ord.Table.Waiter.UserDetails.LastName,
                };
                result.Add(mappedDish);
            }

            return result;
        }
    }
}