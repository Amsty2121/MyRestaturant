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
    public class GetIngredientsListQuery : IRequest<IEnumerable<Ingredient>>
    {

    }

    public class GetIngredientsListHandler : IRequestHandler<GetIngredientsListQuery, IEnumerable<Ingredient>>
    {
        private readonly IGenericRepository<Ingredient> _ingredientsRepository;
        public GetIngredientsListHandler(IGenericRepository<Ingredient> ingredientsRepository, IGenericRepository<IngredientStatus> ingredientStatusesRepository)
        {
            _ingredientsRepository = ingredientsRepository;
        }
        public async Task<IEnumerable<Ingredient>> Handle(GetIngredientsListQuery request, CancellationToken cancellationToken)
        {
            return await _ingredientsRepository.GetAllWithInclude(x=>x.IngredientStatus);

            
        }
    }
}