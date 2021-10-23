using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks; 

namespace Application.TableStatuses.Queries.GetTableStatusById
{
    public class GetTableStatusByIdQuery : IRequest<TableStatus>
    {
        public int TableStatusId { get; set; }
    }

    public class GetTableStatusByIdQueryHandler : IRequestHandler<GetTableStatusByIdQuery, TableStatus>
    {
        private readonly IGenericRepository<TableStatus> _tableStatusRepository;

        public GetTableStatusByIdQueryHandler(IGenericRepository<TableStatus> tableStatusRepository)
        {
            _tableStatusRepository = tableStatusRepository;
        }

        public async Task<TableStatus> Handle(GetTableStatusByIdQuery request, CancellationToken cancellationToken)
        {
            TableStatus tableStatus = await _tableStatusRepository.GetByIdWithInclude(request.TableStatusId, x => x.Tables);

            if (tableStatus == null)
            {
                throw new EntityDoesNotExistException("The TableStatus does not exist");
            }

            return tableStatus;
        }
    }
}