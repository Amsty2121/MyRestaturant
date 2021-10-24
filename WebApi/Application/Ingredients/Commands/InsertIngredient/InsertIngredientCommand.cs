using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Common.Dto.Ingredients;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Ingredients.Commands.InsertIngredient
{
    public class InsertIngredientCommand : IRequest<Ingredient>
    {
        public InsertIngredientDto Dto { get; set; }
    }

    public class InsertIngredientCommandHandler : IRequestHandler<InsertIngredientCommand, Ingredient>
    {
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly IGenericRepository<IngredientStatus> _ingredientStatusRepository;

        public InsertIngredientCommandHandler(IGenericRepository<Ingredient> ingredientRepository,
                                              IGenericRepository<IngredientStatus> ingredientStatusRepository)
        {
            _ingredientRepository = ingredientRepository;
            _ingredientStatusRepository = ingredientStatusRepository;
        }

        public async Task<Ingredient> Handle(InsertIngredientCommand request, CancellationToken cancellationToken)
        {
            Ingredient ingredient = await _ingredientRepository.FirstOrDefault(x => x.IngredientName == request.Dto.IngredientName);
            if (ingredient != null)
            {
                throw new EntityAlreadyExistsException("This Ingredient already exists");
            }

            IngredientStatus ingredientStatus =
                await _ingredientStatusRepository.GetById(request.Dto.IngredientStatusId);
            if (ingredientStatus == null)
            {
                throw new EntityDoesNotExistException("This IngredientStatus does not exist");
            }

            ingredient = new Ingredient()
            {
                IngredientName = request.Dto.IngredientName,
                IngredientDescription = request.Dto.IngredientDescription,
                IngredientStatus = ingredientStatus
            };

            await _ingredientRepository.Add(ingredient);


            return ingredient;
        }
    }
}