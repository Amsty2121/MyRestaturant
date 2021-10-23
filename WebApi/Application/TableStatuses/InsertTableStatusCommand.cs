using Application.Common.Interfaces;
using Common.Dto.TableStatuses;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TableStatuses
{
    public class InsertTableStatusCommand : IRequest<TableStatus>
    {
        public InsertTableStatusDto Dto { get; set; }
    }

    public class InsertTableStatusCommandHandler : IRequestHandler<InsertTableStatusCommand, TableStatus>
    {
        private readonly IGenericRepository<TableStatus> _tableStatusRepository;

        public InsertTableStatusCommandHandler(IGenericRepository<TableStatus> tableStatusRepository)
        {
            _tableStatusRepository = tableStatusRepository;
        }

        public async Task<TableStatus> Handle(InsertTableStatusCommand request, CancellationToken cancellationToken)
        {
            TableStatus tableStatus = await _tableStatusRepository.FirstOrDefault(x => x.TableStatusName == request.Dto.TableStatusName);
            if (tableStatus != null)
            {
                throw new EntityAlreadyExistsException("This TableStatus already exists");
            }

            tableStatus = new TableStatus()
            {
                TableStatusName = request.Dto.TableStatusName
            };

            await _tableStatusRepository.Add(tableStatus); 

            return tableStatus;
        }
    }
}