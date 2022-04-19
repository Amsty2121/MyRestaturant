using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Common.Dto.Dishes;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Dishes.Commands.InsertDish
{
    public class InsertDishCommand : IRequest<Dish>
    {
        public InsertDishDto Dto { get; set; }
    }

    public class InsertDishCommandHandler : IRequestHandler<InsertDishCommand, Dish>
    {
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly IGenericRepository<DishStatus> _dishStatusRepository;
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;
        public InsertDishCommandHandler(IGenericRepository<Dish> dishRepository,
                                        IGenericRepository<Ingredient> ingredientRepository,
                                        IGenericRepository<DishStatus> dishStatusRepository,
                                        IGenericRepository<DishCategory> dishCategoryRepository)
        {
            _dishRepository = dishRepository;
            _ingredientRepository = ingredientRepository;
            _dishStatusRepository = dishStatusRepository;
            _dishCategoryRepository = dishCategoryRepository;
        }

        public async Task<Dish> Handle(InsertDishCommand request, CancellationToken cancellationToken)
        {
            Dish dish = await _dishRepository.FirstOrDefault(x => x.DishName == request.Dto.DishName);

            if (dish != null)
            {
                throw new EntityAlreadyExistsException("This Dish already exists");
            }

            DishStatus dishStatus = await _dishStatusRepository.GetById(request.Dto.DishStatusId);

            if (dishStatus == null)
            {
                throw new EntityDoesNotExistException("This DishStatus does not exist");
            }

            DishCategory dishCategory = await _dishCategoryRepository.GetById(request.Dto.DishCategoryId);

            if (dishCategory == null)
            {
                throw new EntityDoesNotExistException("This DishCategory does not exist");
            }

            ICollection<Ingredient> ingredients = new List<Ingredient>();

            foreach (var id in request.Dto.IngredientsId)
            {
                var newIngredient = await _ingredientRepository.GetById(id);
                if (newIngredient == null)
                {
                    throw new EntityDoesNotExistException("The Ingredient does not exist");
                }
                ingredients.Add(newIngredient);
            }

            var dishIngredients = new List<DishIngredient>();

            foreach (var ingredient in ingredients)
            {
                var newDishIngredient = new DishIngredient() { IngredientId = ingredient.Id, };

                dishIngredients.Add(newDishIngredient);
            }

            dish = new Dish()
            {
                DishName = request.Dto.DishName,
                DishDescription = request.Dto.DishDescription,
                DishStatusId = request.Dto.DishStatusId,
                DishPrice = request.Dto.DishPrice,
                DishCategoryId = request.Dto.DishCategoryId,
                DishIngredients = dishIngredients
            };

            await _dishRepository.Add(dish);

            return dish;
        }
    }
}