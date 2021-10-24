using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Ingredients;

namespace Application.Ingredients.Queries.GetIngredientsList
{
    public class GetIngredientsListQuery : IRequest<IEnumerable<IngredientsWithStatuses>>
    {

    }

    public class GetIngredientsListHandler : IRequestHandler<GetIngredientsListQuery, IEnumerable<IngredientsWithStatuses>>
    {
        private readonly IGenericRepository<Ingredient> _ingredientsRepository;
        private readonly IGenericRepository<IngredientStatus> _ingredientStatusesRepository;
        public GetIngredientsListHandler(IGenericRepository<Ingredient> ingredientsRepository, IGenericRepository<IngredientStatus> ingredientStatusesRepository)
        {
            _ingredientsRepository = ingredientsRepository;
            _ingredientStatusesRepository = ingredientStatusesRepository;
        }
        public async Task<IEnumerable<IngredientsWithStatuses>> Handle(GetIngredientsListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Ingredient> ingredients = await _ingredientsRepository.GetAll();

            List<IngredientsWithStatuses> ingredientsWithStatuses = new List<IngredientsWithStatuses>();

            foreach (var ingredient in ingredients)
            {
                var ingredientStatus = await _ingredientStatusesRepository.GetById(ingredient.IngredientStatusId);

                ingredientsWithStatuses.Add(new IngredientsWithStatuses()
                {
                    Id = ingredient.Id,
                    IngredientName = ingredient.IngredientName,
                    IngredientDescription = ingredient.IngredientDescription,
                    IngredientStatusId = ingredient.IngredientStatusId,
                    IngredientStatusName = ingredientStatus.IngredientStatusName
                });
            }

            return ingredientsWithStatuses;
        }
    }
}