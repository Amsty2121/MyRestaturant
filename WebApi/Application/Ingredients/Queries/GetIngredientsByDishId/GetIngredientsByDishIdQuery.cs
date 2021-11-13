using Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Ingredients;
using Domain.Entities;

namespace Application.Ingredients.Queries.GetIngredientsByDishId
{
    public class GetIngredientsByDishIdQuery : IRequest<ICollection<GetIngredientDto>>
    {
        public int DishId { get; set; }
    }

    public class GetIngredientsByDishIdQueryHandler : IRequestHandler<GetIngredientsByDishIdQuery, ICollection<GetIngredientDto>>
    {
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<DishIngredient> _dishIngredientRepository;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;

        public GetIngredientsByDishIdQueryHandler(IGenericRepository<Dish> dishRepository,
                                                  IGenericRepository<DishIngredient> dishIngredientRepository,
                                                  IGenericRepository<Ingredient> ingredientRepository)
        {
            _dishRepository = dishRepository;
            _dishIngredientRepository = dishIngredientRepository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<ICollection<GetIngredientDto>> Handle(GetIngredientsByDishIdQuery request,
            CancellationToken cancellationToken)
        {
            var dishes = await _dishRepository.GetByIdWithInclude(request.DishId,x=>x.DishIngredients);

            ICollection<GetIngredientDto> result = new List<GetIngredientDto>();

            foreach (var ingredient in dishes.DishIngredients)
            {
                var ingr = await _ingredientRepository.GetByIdWithInclude(ingredient.IngredientId, x => x.IngredientStatus);
                var mappedIngredient = new GetIngredientDto()
                {
                    Id = ingr.Id,
                    IngredientName = ingr.IngredientName,
                    IngredientDescription = ingr.IngredientDescription,
                    IngredientStatusId = ingr.IngredientStatusId,
                    IngredientStatusName = ingr.IngredientStatus.IngredientStatusName
                };
                result.Add(mappedIngredient);
            }

            return result;
        }
    }
}