using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Ingredients;
using Common.Dto.IngredientStatuses;

namespace Application.IngredientStatuses.Queries.GetStatusByIngredientId
{
    public class GetStatusByIngredientIdQuery : IRequest<IngredientStatus>
    {
        public int IngredientId { get; set; }
    }

    public class GetIngredientStatusByIdQueryHandler : IRequestHandler<GetStatusByIngredientIdQuery, IngredientStatus>
    {
        private readonly IGenericRepository<Ingredient> _ingredientRepository;

        public GetIngredientStatusByIdQueryHandler(IGenericRepository<Ingredient> ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<IngredientStatus> Handle(GetStatusByIngredientIdQuery request,
            CancellationToken cancellationToken)
        {
            var ingredient = await _ingredientRepository.GetByIdWithInclude(request.IngredientId,x=>x.IngredientStatus);

            return ingredient.IngredientStatus;
        }
    }
}