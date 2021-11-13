using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TableStatuses.Queries.GetStatusByTableId
{
    public class GetStatusByTableIdQuery : IRequest<TableStatus>
    {
        public int TableId { get; set; }
    }

    public class GetTableStatusByIdQueryHandler : IRequestHandler<GetStatusByTableIdQuery, TableStatus>
    {
        private readonly IGenericRepository<Table> _tableRepository;

        public GetTableStatusByIdQueryHandler(IGenericRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<TableStatus> Handle(GetStatusByTableIdQuery request,
            CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetByIdWithInclude(request.TableId, x => x.TableStatus);

            return table.TableStatus;
        }
    }
}