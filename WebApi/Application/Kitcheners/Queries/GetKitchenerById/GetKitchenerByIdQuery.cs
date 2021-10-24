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
    public class KitchenerWithOrders
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<int> OrdersId { get; set; }

}

    public class GetKitchenerByIdQuery : IRequest<KitchenerWithOrders>
    {
        public int KitchenerId { get; set; }
    }

    public class GetKitchenerByIdQueryHandler : IRequestHandler<GetKitchenerByIdQuery, KitchenerWithOrders>
    {
        private readonly IGenericRepository<Kitchener> _kitchenerRepository;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<Order> _orderRepository;

        public GetKitchenerByIdQueryHandler(IGenericRepository<Kitchener> kitchenerRepository,
            IGenericRepository<Dish> dishRepository,
                                         IGenericRepository<Order> orderRepository)
        {
            _kitchenerRepository = kitchenerRepository;
            _dishRepository = dishRepository;
            _orderRepository = orderRepository;
        }

        public async Task<KitchenerWithOrders> Handle(GetKitchenerByIdQuery request, CancellationToken cancellationToken)
        {
            Kitchener kitchener = await _kitchenerRepository.GetByIdWithInclude(request.KitchenerId, x => x.UserDetails);
            if (kitchener == null)
            {
                throw new EntityDoesNotExistException("The Kitchener does not exist");
            }

            var orders = (await _orderRepository.GetWhere(x => x.KitchenerId == kitchener.Id)).ToList();


            var kitchenerWithOrders = new KitchenerWithOrders()
            {
                Id = kitchener.Id,
                FirstName = kitchener.UserDetails.FirstName,
                LastName = kitchener.UserDetails.LastName, 
                OrdersId = orders.Select(x=>x.Id).ToList()
            };

            return kitchenerWithOrders;
        }
    }
}
