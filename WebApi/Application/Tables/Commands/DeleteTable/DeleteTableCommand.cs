using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tables.Commands.DeleteTable
{
    public class DeleteTableCommand : IRequest<bool>
    {
        public int TableId { get; set; }
    }

    public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand, bool>
    {
        private readonly IGenericRepository<Table> _tableRepository;

        public DeleteTableCommandHandler(IGenericRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }
        public async Task<bool> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            Table deletedTable = await _tableRepository.FirstOrDefault(x => x.Id == request.TableId);

            if (deletedTable != null)
            {
                await _tableRepository.Remove(deletedTable);
                return true;
            }

            return false;
        }
    }
}