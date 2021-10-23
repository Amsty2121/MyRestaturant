using Application.Common.Interfaces;
using Common.Dto.IngredientStatuses;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.IngredientStatuses
{
    public class InsertIngredientStatusCommand : IRequest<IngredientStatus>
    {
        public InsertIngredientStatusDto Dto { get; set; }
    }

    public class InsertIngredientStatusCommandHandler : IRequestHandler<InsertIngredientStatusCommand, IngredientStatus>
    {
        private readonly IGenericRepository<IngredientStatus> _ingredientStatusRepository;

        public InsertIngredientStatusCommandHandler(IGenericRepository<IngredientStatus> ingredientStatusRepository)
        {
            _ingredientStatusRepository = ingredientStatusRepository;
        }

        public async Task<IngredientStatus> Handle(InsertIngredientStatusCommand request, CancellationToken cancellationToken)
        {
            IngredientStatus ingredientStatus = await _ingredientStatusRepository.FirstOrDefault(x => x.IngredientStatusName == request.Dto.IngredientStatusName);
            if (ingredientStatus != null)
            {
                throw new EntityAlreadyExistsException("This IngredientStatus already exists");
            }

            ingredientStatus = new IngredientStatus()
            {
                IngredientStatusName = request.Dto.IngredientStatusName
            };

            await _ingredientStatusRepository.Add(ingredientStatus);

            return ingredientStatus;
        }
    }
}