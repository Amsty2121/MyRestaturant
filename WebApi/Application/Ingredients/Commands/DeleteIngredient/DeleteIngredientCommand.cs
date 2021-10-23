using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks; 

namespace Application.Ingredients.Commands.DeleteIngredient
{
    public class DeleteIngredientCommand : IRequest<bool>
    {
        public int IngredientId { get; set; }
    }

    public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, bool>
    {
        private readonly IGenericRepository<Ingredient> _ingredientRepository;

        public DeleteIngredientCommandHandler(IGenericRepository<Ingredient> ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<bool> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            Ingredient deletedIngredient = await _ingredientRepository.FirstOrDefault(x => x.Id == request.IngredientId);

            if (deletedIngredient == null)
            {
                return false;
            }

            await _ingredientRepository.Remove(deletedIngredient);

            return true;
        }
    }
}