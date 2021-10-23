using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DishCategories.Queries.GetDishCategoriesList
{
    public class GetDishCategoriesListQuery : IRequest<IEnumerable<DishCategory>>
    {
    }
    
    public class GetDishCategoriesListHandler : IRequestHandler<GetDishCategoriesListQuery, IEnumerable<DishCategory>>
    {
        private readonly IGenericRepository<DishCategory> _dishCategoriesRepository;

        public GetDishCategoriesListHandler(IGenericRepository<DishCategory> dishCategoriesRepository)
        {
            _dishCategoriesRepository = dishCategoriesRepository;
        }
        public async Task<IEnumerable<DishCategory>> Handle(GetDishCategoriesListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<DishCategory> dishCategories = await _dishCategoriesRepository.GetAll();

            return dishCategories;
        }
    }
}