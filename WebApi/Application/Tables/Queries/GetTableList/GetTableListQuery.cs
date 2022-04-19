using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Ingredients.Queries.GetIngredientsList;
using Common.Dto.Tables;

namespace Application.Tables.Queries.GetTableList
{
    public class GetTablesListQuery : IRequest<IEnumerable<TablesWithStatusesAndWaiters>>
    {
    }

    public class GetTableListQueryHandler : IRequestHandler<GetTablesListQuery, IEnumerable<TablesWithStatusesAndWaiters>>
    {
        private readonly IGenericRepository<TableStatus> _tableStatusRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Waiter> _waiterRepository;

        public GetTableListQueryHandler(IGenericRepository<TableStatus> tableStatusRepository, 
                                       IGenericRepository<Table> tableRepository,
                                       IGenericRepository<Waiter> waiterRepository)
        {
            _tableStatusRepository = tableStatusRepository;
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
        }

        public async Task<IEnumerable<TablesWithStatusesAndWaiters>> Handle(GetTablesListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Table> tables = await _tableRepository.GetAll();

            var tablesWithStatusesAndWaiters = new List<TablesWithStatusesAndWaiters>();

            foreach (var table in tables)
            {
                var tableStatus = await _tableStatusRepository.GetById(table.TableStatusId);
                var waiter = await _waiterRepository.GetByIdWithInclude(table.WaiterId,x=> x.UserDetails);

                tablesWithStatusesAndWaiters.Add(new TablesWithStatusesAndWaiters()
                {
                    Id = table.Id,
                    TableDescription = table.TableDescription,
                    TableStatusId = tableStatus.Id,
                    TableStatusName = tableStatus.TableStatusName,
                    WaiterId = waiter.Id,
                    WaiterName = waiter.UserDetails.FirstName + " " + waiter.UserDetails.LastName
                });
            }

            return tablesWithStatusesAndWaiters;
        }
    }
}