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
        private readonly IGenericRepository<DishStatus> _dishStatusRepository;
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;

        public GetDishListQueryHandler(IGenericRepository<Dish> dishRepository, 
                                       IGenericRepository<DishStatus> dishStatusRepository,
                                       IGenericRepository<DishCategory> dishCategoryRepository)
        {
            _dishRepository = dishRepository;
            _dishStatusRepository = dishStatusRepository;
            _dishCategoryRepository = dishCategoryRepository;
        }

        public async Task<IEnumerable<Dish>> Handle(GetDishesListQuery request, CancellationToken cancellationToken)
        {
            return await _dishRepository.GetAllWithInclude(x=>x.DishCategory,x=>x.DishStatus,x=>x.DishIngredients);
        }
    }
}