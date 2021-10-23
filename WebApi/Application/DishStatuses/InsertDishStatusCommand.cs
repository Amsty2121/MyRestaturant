using Application.Common.Interfaces;
using Common.Dto.DishStatuses;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DishStatuses
{
    public class InsertDishStatusCommand : IRequest<DishStatus> 
    {
        public InsertDishStatusDto Dto { get; set; }
    }

    public class InsertDishStatusCommandHandler : IRequestHandler<InsertDishStatusCommand, DishStatus>
    {
        private readonly IGenericRepository<DishStatus> _dishStatusRepository;

        public InsertDishStatusCommandHandler(IGenericRepository<DishStatus> dishStatusRepository)
        {
            _dishStatusRepository = dishStatusRepository;
        }

        public async Task<DishStatus> Handle(InsertDishStatusCommand request, CancellationToken cancellationToken)
        {
            DishStatus dishStatus = await _dishStatusRepository.FirstOrDefault(x => x.DishStatusName == request.Dto.DishStatusName);
            if (dishStatus != null)
            {
                throw new EntityAlreadyExistsException("This DishStatus already exists");
            }

            dishStatus = new DishStatus()
            {
                DishStatusName = request.Dto.DishStatusName
            };

            await _dishStatusRepository.Add(dishStatus);

            return dishStatus;
        }
    }
}