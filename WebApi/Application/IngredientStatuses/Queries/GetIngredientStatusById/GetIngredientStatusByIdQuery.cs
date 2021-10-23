using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.IngredientStatuses.Queries.GetIngredientStatusById
{
    public class GetIngredientStatusByIdQuery : IRequest<IngredientStatus>
    {
        public int IngredientStatusId { get; set; }
    }

    public class GetIngredientStatusByIdQueryHandler : IRequestHandler<GetIngredientStatusByIdQuery, IngredientStatus>
    {
        private readonly IGenericRepository<IngredientStatus> _ingredientStatusRepository;

        public GetIngredientStatusByIdQueryHandler(IGenericRepository<IngredientStatus> ingredientStatusRepository)
        {
            _ingredientStatusRepository = ingredientStatusRepository;
        }

        public async Task<IngredientStatus> Handle(GetIngredientStatusByIdQuery request, CancellationToken cancellationToken)
        {
            IngredientStatus ingredientStatus = await _ingredientStatusRepository.GetById(request.IngredientStatusId);

            if (ingredientStatus == null)
            {
                throw new EntityDoesNotExistException("The IngredientStatus does not exist");
            }

            return ingredientStatus;
        }
    }
}