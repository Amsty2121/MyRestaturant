using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Orders;
using Common.Dto.Tables;


namespace Application.Tables.Queries.GetTableById
{
    
    public class GetTableByIdQuery : IRequest<GetTableDto>
    {
        public int TableId { get; set; }
    }

    class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, GetTableDto>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<TableStatus> _tableStatusRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;

        public GetTableByIdQueryHandler(IGenericRepository<Order> orderRepository,
            IGenericRepository<TableStatus> tableStatusRepository,
            IGenericRepository<Table> tableRepository,
            IGenericRepository<Waiter> waiterRepository)

        {
            _orderRepository = orderRepository;
            _tableStatusRepository = tableStatusRepository;
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
        }

        public async Task<GetTableDto> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            Table table = await _tableRepository.GetByIdWithInclude(request.TableId, x => x.Orders,x=>x.TableStatus,x=>x.Waiter);

            if (table == null)
            {
                throw new EntityDoesNotExistException("The table does not exist");
            }

            var tableStatus = await _tableStatusRepository.GetById(table.TableStatusId);
            var waiter = await _waiterRepository.GetByIdWithInclude(table.WaiterId, x => x.UserDetails);
            var orders = await _orderRepository.GetWhere(x=>x.TableId == request.TableId);



            var getTableDto = new GetTableDto()
            {
                Id = table.Id,
                TableDescription = table.TableDescription,
                TableStatusId = tableStatus.Id,
                TableStatusName = tableStatus.TableStatusName,
                WaiterId = waiter.Id,
                WaiterName = waiter.UserDetails.FirstName + " " + waiter.UserDetails.LastName,

                OrdersId = orders.Select(x=>x.TableId).Distinct().ToList()
            };

            return getTableDto;
        }
    }
}