using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Ingredients;

namespace Application.Ingredients.Commands.UpdateIngredient
{
    public class IngredientStatusUpdating
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        public int IngredientStatusId { get; set; }
        public string IngredientStatusName { get; set; }
    }

    public class UpdateIngredientCommand : IRequest<IngredientStatusUpdating>
    {
        public int Id { get; set; }
        public UpdateIngredientDto Dto { get; set; }
    }

    public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand, IngredientStatusUpdating>
    {
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly IGenericRepository<IngredientStatus> _ingredientStatusRepository;

        public UpdateIngredientCommandHandler(IGenericRepository<Ingredient> ingredientRepository, IGenericRepository<IngredientStatus> ingredientStatusRepository)
        {
            _ingredientRepository = ingredientRepository;
            _ingredientStatusRepository = ingredientStatusRepository;
        }

        public async Task<IngredientStatusUpdating> Handle(UpdateIngredientCommand request,
            CancellationToken cancellationToken)
        {
            Ingredient updatedIngredient =
                await _ingredientRepository.GetByIdWithInclude(request.Id, x => x.IngredientStatus,
                    x => x.DishIngredients);

            if (updatedIngredient == null)
            {
                throw new EntityDoesNotExistException("The Ingredient does not exist");
            }

            var ingredientStatus = await _ingredientStatusRepository.GetById(request.Dto.IngredientStatusId);
            if (ingredientStatus == null)
            {
                throw new EntityDoesNotExistException("The IngredientStatus does not exist");
            }

            updatedIngredient.IngredientStatus = ingredientStatus;
            updatedIngredient.IngredientStatusId = ingredientStatus.Id;


            if (request.Dto.IngredientName != null && request.Dto.IngredientName.Length > 0)
            {
                updatedIngredient.IngredientName = request.Dto.IngredientName;
            }

            if (request.Dto.IngredientDescription != null)
            {
                updatedIngredient.IngredientDescription = request.Dto.IngredientDescription;
            }

            await _ingredientRepository.Update(updatedIngredient);

            IngredientStatusUpdating ingredientStatusUpdating = new IngredientStatusUpdating()
            {
                Id = updatedIngredient.Id,
                IngredientName = updatedIngredient.IngredientName,
                IngredientDescription = updatedIngredient.IngredientDescription,
                IngredientStatusId = updatedIngredient.IngredientStatusId,
                IngredientStatusName = updatedIngredient.IngredientStatus.IngredientStatusName
            };

            return ingredientStatusUpdating;
        }
    }
}