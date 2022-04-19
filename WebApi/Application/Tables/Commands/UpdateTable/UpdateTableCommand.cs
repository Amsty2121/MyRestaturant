using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Tables;

namespace Application.Tables.Commands.UpdateTable
{
    public class UpdateTableCommand : IRequest<TableUpdating>
    {
        public int Id { get; set; }
        public UpdateTableDto Dto { get; set; }
    }

    public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, TableUpdating>
    {
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<TableStatus> _tableStatusRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;
        private readonly IGenericRepository<Order> _orderRepository;

        public UpdateTableCommandHandler(IGenericRepository<Order> orderRepository,
            IGenericRepository<TableStatus> tableStatusRepository,
                                         IGenericRepository<Table> tableRepository,
                                         IGenericRepository<Waiter> waiterRepository)
        {
            _orderRepository = orderRepository;
            _tableStatusRepository = tableStatusRepository;
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
        }

        public async Task<TableUpdating> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            Table updatedTable = await _tableRepository.GetByIdWithInclude(request.Id, x => x.TableStatus,x=>x.Waiter,x=>x.Orders);

            if (updatedTable == null)
            {
                throw new EntityDoesNotExistException("The Table does not exist");
            }

            var tableStatus = await _tableStatusRepository.GetById(updatedTable.TableStatusId);
            if (tableStatus == null)
            { 
                throw new EntityDoesNotExistException("The TableStatus does not exist");
            }
            updatedTable.TableStatus = tableStatus;
            updatedTable.TableStatusId = tableStatus.Id;


            var waiter = await _waiterRepository.GetByIdWithInclude(updatedTable.WaiterId, x => x.UserDetails);
            if (waiter == null)
            {
                throw new EntityDoesNotExistException("The Waiter does not exist");
            }
            updatedTable.Waiter = waiter;
            updatedTable.WaiterId = waiter.Id;


            if (request.Dto.TableDescription != null)
            {
                updatedTable.TableDescription = request.Dto.TableDescription;
            }

            await _tableRepository.Update(updatedTable);

            var tableUpdating = new TableUpdating()
            {
                Id = updatedTable.Id,
                TableDescription = updatedTable.TableDescription,
                WaiterId = updatedTable.WaiterId,
                WaiterName = updatedTable.Waiter.UserDetails.FirstName + " " + updatedTable.Waiter.UserDetails.LastName,
                TableStatusId = updatedTable.TableStatusId, 
                TableStatusName = updatedTable.TableStatus.TableStatusName
            };

            return tableUpdating;
        }
    }
}
