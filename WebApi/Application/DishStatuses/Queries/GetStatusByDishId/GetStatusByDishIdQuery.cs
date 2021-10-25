using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Dishes;
using Common.Dto.DishStatuses;

namespace Application.DishStatuses.Queries.GetStatusByDishId
{
    public class GetStatusByDishIdQuery : IRequest<DishStatus>
    {
        public int DishId { get; set; }
    }

    public class GetDishStatusByIdQueryHandler : IRequestHandler<GetStatusByDishIdQuery, DishStatus>
    {
        private readonly IGenericRepository<DishStatus> _dishStatusRepository;
        private readonly IGenericRepository<Dish> _dishRepository;

        public GetDishStatusByIdQueryHandler(IGenericRepository<DishStatus> dishStatusRepository, IGenericRepository<Dish> dishRepository)
        {
            _dishStatusRepository = dishStatusRepository;
            _dishRepository = dishRepository;
        }

        public async Task<DishStatus> Handle(GetStatusByDishIdQuery request,
            CancellationToken cancellationToken)
        {
            var dish = await _dishRepository.GetByIdWithInclude(request.DishId, x => x.DishStatus);

            return dish.DishStatus;
        }
    }
}