using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DishStatuses.Queries.GetDishStatusesList
{
    public class GetDishStatusesListQuery : IRequest<IEnumerable<DishStatus>>
    {
    }

    public class GetDishStatusesListHandler : IRequestHandler<GetDishStatusesListQuery, IEnumerable<DishStatus>>
    {
        private readonly IGenericRepository<DishStatus> _dishStatusesRepository;

        public GetDishStatusesListHandler(IGenericRepository<DishStatus> dishStatusesRepository)
        {
            _dishStatusesRepository = dishStatusesRepository;
        }
        public async Task<IEnumerable<DishStatus>> Handle(GetDishStatusesListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<DishStatus> dishStatuses = await _dishStatusesRepository.GetAll();

            return dishStatuses;
        }
    }
}