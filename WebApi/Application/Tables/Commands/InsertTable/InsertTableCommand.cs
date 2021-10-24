using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Dishes.Commands.InsertDish;
using Common.Dto.Dishes;
using Common.Dto.Orders;
using Common.Dto.Tables;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Tables.Commands.InsertTable
{
    public class InsertTableCommand : IRequest<Table>
    {
        public InsertTableDto Dto { get; set; }
    }

    public class InsertTableCommandHandler : IRequestHandler<InsertTableCommand, Table>
    {
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<TableStatus> _tableStatusRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;
        public InsertTableCommandHandler(IGenericRepository<Table> tableRepository,
                                         IGenericRepository<Waiter> waiterRepository,
                                         IGenericRepository<TableStatus> tableStatusRepository)
        {
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
            _tableStatusRepository = tableStatusRepository;
        }

        public async Task<Table> Handle(InsertTableCommand request, CancellationToken cancellationToken)
        {
            Waiter waiter = await _waiterRepository.GetById(request.Dto.WaiterId);

            if (waiter == null)
            {
                throw new EntityDoesNotExistException("This Waiter of this Table does not exists");
            }

            TableStatus tableStatus = await _tableStatusRepository.GetById(request.Dto.TableStatusId);

            if (tableStatus == null)
            {
                throw new EntityDoesNotExistException("This TableStatus does not exists");
            }

            var table = new Table()
            {
                TableDescription = request.Dto.TableDescription,
                TableStatus = tableStatus,
                Waiter = waiter
                
            };
            await _tableRepository.Add(table);

            return table;
        }
    }
}