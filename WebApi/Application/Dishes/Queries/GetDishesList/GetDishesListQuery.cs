using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Dishes;

namespace Application.Dishes.Queries.GetDishesList
{
    public class GetDishesListQuery : IRequest<IEnumerable<Dish>>
    {
    }
    
    public class GetDishListQueryHandler : IRequestHandler<GetDishesListQuery, IEnumerable<Dish>>
    {
        private readonly IGenericRepository<Dish> _dishRepository;

        public GetDishListQueryHandler(IGenericRepository<Dish> dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<IEnumerable<Dish>> Handle(GetDishesListQuery request, CancellationToken cancellationToken)
        {
            return await _dishRepository.GetAll();
        }
    }
}