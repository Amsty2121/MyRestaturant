using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Dishes;
using Common.Dto.DishCategories;

namespace Application.DishCategories.Queries.GetCategoryByDishId
{
    public class GetCategoryByDishIdQuery : IRequest<DishCategory>
    {
        public int DishId { get; set; }
    }

    public class GetDishCategoryByIdQueryHandler : IRequestHandler<GetCategoryByDishIdQuery, DishCategory>
    {
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;
        private readonly IGenericRepository<Dish> _dishRepository;

        public GetDishCategoryByIdQueryHandler(IGenericRepository<DishCategory> dishCategoryRepository, IGenericRepository<Dish> dishRepository)
        {
            _dishCategoryRepository = dishCategoryRepository;
            _dishRepository = dishRepository;
        }

        public async Task<DishCategory> Handle(GetCategoryByDishIdQuery request,
            CancellationToken cancellationToken)
        {
            var dish = await _dishRepository.GetByIdWithInclude(request.DishId, x => x.DishCategory);

            return dish.DishCategory;
        }
    }
}