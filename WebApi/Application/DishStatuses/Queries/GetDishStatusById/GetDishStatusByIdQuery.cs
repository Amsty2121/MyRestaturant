using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DishStatuses.Queries.GetDishStatusById
{
    public class GetDishStatusByIdQuery : IRequest<DishStatus>
    {
        public int DishStatusId { get; set; }
    }

    public class GetDishStatusByIdQueryHandler : IRequestHandler<GetDishStatusByIdQuery, DishStatus>
    {
        private readonly IGenericRepository<DishStatus> _dishStatusRepository;

        public GetDishStatusByIdQueryHandler(IGenericRepository<DishStatus> dishStatusRepository)
        {
            _dishStatusRepository = dishStatusRepository;
        }

        public async Task<DishStatus> Handle(GetDishStatusByIdQuery request, CancellationToken cancellationToken)
        {
            DishStatus dishStatus = await _dishStatusRepository.GetById(request.DishStatusId);

            if (dishStatus == null)
            {
                throw new EntityDoesNotExistException("The DishStatus does not exist");
            }

            return dishStatus;
        }
    }
}