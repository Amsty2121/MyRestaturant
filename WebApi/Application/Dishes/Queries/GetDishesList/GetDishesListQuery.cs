using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Ingredients.Queries.GetIngredientsList;

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
            IEnumerable<Dish> dish = await _dishRepository.GetAllWithInclude(x => x.DishCategory, x => x.DishStatus);

            return dish;
        }
    }
}