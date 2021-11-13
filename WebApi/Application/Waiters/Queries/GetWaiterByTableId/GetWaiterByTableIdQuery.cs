using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Waiters;

namespace Application.Waiters.Queries.GetWaiterByTableId
{
    public class GetWaiterByTableIdQuery : IRequest<GetWaiterDto>
    {
        public int TableId { get; set; }
    }

    public class GetWaiterByIdQueryHandler : IRequestHandler<GetWaiterByTableIdQuery, GetWaiterDto>
    {
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;

        public GetWaiterByIdQueryHandler(IGenericRepository<Table> tableRepository,
            IGenericRepository<Waiter> waiterRepository)
        {
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
        }

        public async Task<GetWaiterDto> Handle(GetWaiterByTableIdQuery request,
            CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetByIdWithInclude(request.TableId, x => x.Waiter);
            var waiter = await _waiterRepository.GetByIdWithInclude(table.WaiterId, x => x.UserDetails);

            var getWaiterDto = new GetWaiterDto()
            {
                Id = waiter.Id,
                FirstName = waiter.UserDetails.FirstName,
                LastName = waiter.UserDetails.LastName
            };
            return getWaiterDto;
        }
    }
}