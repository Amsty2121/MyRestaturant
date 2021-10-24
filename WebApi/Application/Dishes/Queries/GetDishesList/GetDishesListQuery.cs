using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Dishes;

namespace Application.Dishes.Queries.GetDishesList
{
    public class GetDishesListQuery : IRequest<IEnumerable<DishesWithStatusesAndCategories>>
    {
    }
    
    public class GetDishListQueryHandler : IRequestHandler<GetDishesListQuery, IEnumerable<DishesWithStatusesAndCategories>>
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

        public async Task<IEnumerable<DishesWithStatusesAndCategories>> Handle(GetDishesListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Dish> dishes = await _dishRepository.GetAll();

            var dishesWithStatusesAndCategories = new List<DishesWithStatusesAndCategories>();

            foreach (var dish in dishes)
            {
                var dishStatus = await _dishStatusRepository.GetById(dish.DishStatusId);
                var dishCategory = await _dishCategoryRepository.GetById(dish.DishCategoryId);

                dishesWithStatusesAndCategories.Add(new DishesWithStatusesAndCategories()
                {
                    Id = dish.Id,
                    DishDescription = dish.DishDescription,
                    DishName = dish.DishName,
                    DishPrice = dish.DishPrice,
                    DishCategoryId = dish.DishCategoryId,
                    DishStatusId = dish.DishStatusId,
                    DishCategoryName = dishCategory.DishCategoryName,
                    DishStatusName = dishStatus.DishStatusName,
                });
            }

            return dishesWithStatusesAndCategories;
        }
    }
}