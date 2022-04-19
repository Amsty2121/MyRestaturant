using Application.Common.Interfaces;
using Common.Dto.Dishes;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Dishes.Commands.UpdateDish
{
    public class UpdateDishCommand : IRequest<Dish>
    {
        public int Id { get; set; }
        public UpdateDishDto Dto { get; set; }
    }

    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, Dish>
    {
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly IDishUnitOfWork _dishUnitOfWork;
        private readonly IGenericRepository<DishStatus> _dishStatusRepository;
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;

        public UpdateDishCommandHandler(IGenericRepository<Dish> dishRepository,
            IGenericRepository<Ingredient> ingredientRepository,
            IDishUnitOfWork dishUnitOfWork,
            IGenericRepository<DishStatus> dishStatusRepository,
            IGenericRepository<DishCategory> dishCategoryRepository)
        {
            _dishRepository = dishRepository;
            _ingredientRepository = ingredientRepository;
            _dishUnitOfWork = dishUnitOfWork;
            _dishStatusRepository = dishStatusRepository;
            _dishCategoryRepository = dishCategoryRepository;
        }

        public async Task<Dish> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            Dish updatedDish = await _dishRepository.GetByIdWithInclude(request.Id, x => x.DishIngredients);

            if (updatedDish == null)
            {
                throw new EntityDoesNotExistException("The Dish does not exist");
            }

            var dishStatus = await _dishStatusRepository.GetById(request.Dto.DishStatusId);
            if (dishStatus == null)
            {
                throw new EntityDoesNotExistException("The DishStatus does not exist");
            }

            updatedDish.DishStatus = dishStatus;
            updatedDish.DishStatusId = dishStatus.Id;

            var dishCategory = await _dishCategoryRepository.GetById(request.Dto.DishCategoryId);
            if (dishCategory == null)
            {
                throw new EntityDoesNotExistException("The DishCategory does not exist");
            }

            updatedDish.DishCategory = dishCategory;
            updatedDish.DishCategoryId = dishCategory.Id;


            if (request.Dto.DishName != null && request.Dto.DishName.Length > 0)
            {
                updatedDish.DishName = request.Dto.DishName;
            }

            if (request.Dto.DishDescription != null)
            {
                updatedDish.DishDescription = request.Dto.DishDescription;
            }

            if (request.Dto.DishPrice > 0)
            {
                updatedDish.DishPrice = request.Dto.DishPrice;
            }

            updatedDish.DishIngredients = new List<DishIngredient>();
            await _dishRepository.Update(updatedDish);

            if (request.Dto.IngredientsId != null)
            {
                foreach (var id in request.Dto.IngredientsId)
                {
                    var newIngredient = await _ingredientRepository.GetById(id);
                    if (newIngredient == null)
                    {
                        throw new EntityDoesNotExistException("The Ingredient does not exist");
                    }
                }

                foreach (var ingredientId in request.Dto.IngredientsId)
                {
                    bool dishIngredientNotExists = await _dishUnitOfWork.DishIngredientRepository.FirstOrDefault(x =>
                        x.DishId == updatedDish.Id && x.IngredientId == ingredientId) == null;

                    if (dishIngredientNotExists)
                    {
                        await _dishUnitOfWork.DishIngredientRepository.Add(new DishIngredient()
                        {
                            DishId = updatedDish.Id,
                            IngredientId = ingredientId,
                        });
                    }

                    _dishUnitOfWork.Save();
                }
            }

            return updatedDish;
        }
    }
}
