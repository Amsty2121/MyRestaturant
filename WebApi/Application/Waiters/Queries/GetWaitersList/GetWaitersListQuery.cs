using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Waiters.Queries.GetWaitersList
{
    public class GetWaitersListQuery : IRequest<IEnumerable<Waiter>>
    {
    }

    public class GetWaitersListQueryHandler : IRequestHandler<GetWaitersListQuery, IEnumerable<Waiter>>
    {
        private readonly IGenericRepository<Waiter> _waiterRepository;


        public GetWaitersListQueryHandler(IGenericRepository<Waiter> waiterRepository)
        {
            _waiterRepository = waiterRepository;
        }

        public async Task<IEnumerable<Waiter>> Handle(GetWaitersListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Waiter> waiters = await _waiterRepository.GetAllWithInclude(x => x.UserDetails);

            return waiters;
        }
    }
}