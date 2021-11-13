using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Ingredients;

namespace Application.Ingredients.Queries.GetIngredientById
{
    public class GetIngredientByIdQuery : IRequest<GetIngredientDto>
    {
        public int IngredientId { get; set; }
    }

    public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, GetIngredientDto>
    {
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly IGenericRepository<IngredientStatus> _ingredientStatusRepository;

        public GetIngredientByIdQueryHandler(IGenericRepository<Ingredient> ingredientRepository, IGenericRepository<IngredientStatus> ingredientStatusRepository)
        {
            _ingredientRepository = ingredientRepository;
            _ingredientStatusRepository = ingredientStatusRepository;
        }

        public async Task<GetIngredientDto> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            Ingredient ingredient = await _ingredientRepository.GetByIdWithInclude(request.IngredientId, x => x.IngredientStatus);

            if (ingredient == null)
            {
                throw new EntityDoesNotExistException("The Ingredient does not exist");
            }

            IngredientStatus ingredientStatus = await _ingredientStatusRepository.GetById(ingredient.IngredientStatusId);

            var getIngredientDto = new GetIngredientDto()
            {
                Id = ingredient.Id,
                IngredientName = ingredient.IngredientName,
                IngredientDescription = ingredient.IngredientDescription,
                IngredientStatusId = ingredient.IngredientStatusId,
                IngredientStatusName = ingredientStatus.IngredientStatusName
            };

            return getIngredientDto;
        }
    }
}