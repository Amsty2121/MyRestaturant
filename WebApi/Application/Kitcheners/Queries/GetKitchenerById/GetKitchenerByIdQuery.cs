using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Kitcheners.Queries.GetKitchenerById
{
    public class KitchenerWithDishesAndOrders
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<int> DishOrdersId { get; set; }

}

    public class GetKitchenerByIdQuery : IRequest<KitchenerWithDishesAndOrders>
    {
        public int KitchenerId { get; set; }
    }

    public class GetKitchenerByIdQueryHandler : IRequestHandler<GetKitchenerByIdQuery, KitchenerWithDishesAndOrders>
    {
        private readonly IGenericRepository<Kitchener> _kitchenerRepository;
        private readonly IGenericRepository<DishOrder> _dishOrderRepository;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<Order> _orderRepository;

        public GetKitchenerByIdQueryHandler(IGenericRepository<Kitchener> kitchenerRepository,
                                         IGenericRepository<DishOrder> dishOrderRepository,
                                         IGenericRepository<Dish> dishRepository,
                                         IGenericRepository<Order> orderRepository)
        {
            _kitchenerRepository = kitchenerRepository;
            _dishRepository = dishRepository;
            _orderRepository = orderRepository;
            _dishOrderRepository = dishOrderRepository;
        }

        public async Task<KitchenerWithDishesAndOrders> Handle(GetKitchenerByIdQuery request, CancellationToken cancellationToken)
        {
            Kitchener kitchener = await _kitchenerRepository.GetByIdWithInclude(request.KitchenerId, x => x.UserDetails, x=>x.DishOrders);
            if (kitchener == null)
            {
                throw new EntityDoesNotExistException("The Kitchener does not exist");
            }

            var dishOrders = (await _dishOrderRepository.GetWhere(x => x.KitchenerId == kitchener.Id)).ToList();


            var kitchenerWithDishesAndOrders = new KitchenerWithDishesAndOrders()
            {
                Id = kitchener.Id,
                FirstName = kitchener.UserDetails.FirstName,
                LastName = kitchener.UserDetails.LastName,
                DishOrdersId = dishOrders.Select(x=>x.Id).ToList()
            };

            return kitchenerWithDishesAndOrders;
        }
    }
}
