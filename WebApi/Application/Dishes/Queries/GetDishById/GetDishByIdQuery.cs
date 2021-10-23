using System.Collections.Generic;
using Application.Common.Interfaces;
using Common.Dto.Dishes;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Dishes.Queries.GetDishById
{
    public class DishWithStatusAndCategory
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public int DishPrice { get; set; }
        public string DishDescription { get; set; }
        public int DishStatusId { get; set; }
        public string DishStatusName { get; set; }
        public int DishCategoryId { get; set; }
        public string DishCategoryName { get; set; }
        public ICollection<int> IngredientsId { get; set; }
    }
    public class GetDishByIdQuery : IRequest<DishWithStatusAndCategory>
    {
        public int DishId { get; set; }
    }

    class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, DishWithStatusAndCategory>
    {
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<DishIngredient> _dishIngredientRepository;
        private readonly IGenericRepository<DishStatus> _dishStatusRepository;
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;
        public GetDishByIdQueryHandler(IGenericRepository<Dish> dishRepository,
                                         IGenericRepository<DishIngredient> dishIngredientRepository,
                                         IGenericRepository<DishStatus> dishStatusRepository,
                                         IGenericRepository<DishCategory> dishCategoryRepository)
        {
            _dishRepository = dishRepository;
            _dishIngredientRepository = dishIngredientRepository;
            _dishStatusRepository = dishStatusRepository;
            _dishCategoryRepository = dishCategoryRepository;
        }
        public async Task<DishWithStatusAndCategory> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            Dish dish = await _dishRepository.GetByIdWithInclude(request.DishId, x => x.DishIngredients);
            if (dish == null)
            {
                throw new EntityDoesNotExistException("The Dish does not exist");
            }

            var dishIngredients = (await _dishIngredientRepository.GetWhere(x => x.DishId == request.DishId)).ToList();

            var dishStatus = await _dishStatusRepository.GetById(dish.DishStatusId);
            var dishCategory = await _dishCategoryRepository.GetById(dish.DishCategoryId);

            var dishWithStatusAndCategory = new DishWithStatusAndCategory()
            {
                Id = dish.Id,
                DishDescription = dish.DishDescription,
                DishName = dish.DishName,
                DishPrice = dish.DishPrice,
                DishCategoryId = dish.DishCategoryId,
                DishStatusId = dish.DishStatusId,
                DishCategoryName = dishCategory.DishCategoryName,
                DishStatusName = dishStatus.DishStatusName,

                IngredientsId = dishIngredients.Select(x => x.IngredientId).Distinct().ToList(),
            };

            return dishWithStatusAndCategory;
        }
    }
}