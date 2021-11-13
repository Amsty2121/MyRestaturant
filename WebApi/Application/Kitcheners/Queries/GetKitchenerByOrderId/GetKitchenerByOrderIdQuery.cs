using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Kitcheners;

namespace Application.Kitcheners.Queries.GetKitchenerByOrderId
{
    public class GetKitchenerByOrderIdQuery : IRequest<GetKitchenerDto>
    {
        public int OrderId { get; set; }
    }

    public class GetKitchenerByIdQueryHandler : IRequestHandler<GetKitchenerByOrderIdQuery, GetKitchenerDto>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Kitchener> _kitchenerRepository;

        public GetKitchenerByIdQueryHandler(IGenericRepository<Order> orderRepository,
                IGenericRepository<Kitchener> kitchenerRepository)
        {
            _orderRepository = orderRepository;
            _kitchenerRepository = kitchenerRepository;
        }

        public async Task<GetKitchenerDto> Handle(GetKitchenerByOrderIdQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdWithInclude(request.OrderId, x => x.Kitchener);

            GetKitchenerDto getKitchenerDto;

            if (order.KitchenerId != null)
            {
                var kitchener = await _kitchenerRepository.GetByIdWithInclude(order.KitchenerId.Value, y => y.UserDetails);

                getKitchenerDto = new GetKitchenerDto()
                {
                    Id = kitchener.Id,
                    FirstName = kitchener.UserDetails.FirstName,
                    LastName = kitchener.UserDetails.LastName,
                    OrdersId = null
                };
            }
            else
            {
                getKitchenerDto = new GetKitchenerDto()
                {
                    Id = 1000000,
                    FirstName = "not",
                    LastName = "assigned",
                    OrdersId = null
                };
            }
            


            return getKitchenerDto;
        }
    }
}