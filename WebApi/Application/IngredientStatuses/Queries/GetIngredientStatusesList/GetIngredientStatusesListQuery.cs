using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.IngredientStatuses.Queries.GetIngredientStatusesList
{
    public class GetIngredientStatusesListQuery : IRequest<IEnumerable<IngredientStatus>>
    {
    }

    public class GetIngredientStatusesListHandler : IRequestHandler<GetIngredientStatusesListQuery, IEnumerable<IngredientStatus>>
    {
        private readonly IGenericRepository<IngredientStatus> _ingredientStatusesRepository;

        public GetIngredientStatusesListHandler(IGenericRepository<IngredientStatus> ingredientStatusesRepository)
        {
            _ingredientStatusesRepository = ingredientStatusesRepository;
        }
        public async Task<IEnumerable<IngredientStatus>> Handle(GetIngredientStatusesListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<IngredientStatus> ingredientStatuses = await _ingredientStatusesRepository.GetAll();

            return ingredientStatuses;
        }
    }
}