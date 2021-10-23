using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TableStatuses.Queries.GetTableStatusesList
{
    public class GetTableStatusesListQuery : IRequest<IEnumerable<TableStatus>>
    {
    }

    public class GetTableStatusesListHandler : IRequestHandler<GetTableStatusesListQuery, IEnumerable<TableStatus>>
    {
        private readonly IGenericRepository<TableStatus> _tableStatusesRepository;

        public GetTableStatusesListHandler(IGenericRepository<TableStatus> tableStatusesRepository)
        {
            _tableStatusesRepository = tableStatusesRepository;
        }
        public async Task<IEnumerable<TableStatus>> Handle(GetTableStatusesListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<TableStatus> tableStatuses = await _tableStatusesRepository.GetAll();

            return tableStatuses;
        }
    }
}