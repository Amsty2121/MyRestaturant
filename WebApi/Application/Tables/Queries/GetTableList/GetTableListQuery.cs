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
    public class GetTablesListQuery : IRequest<IEnumerable<Table>>
    {
    }

    public class GetTableListQueryHandler : IRequestHandler<GetTablesListQuery, IEnumerable<Table>>
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

        public async Task<IEnumerable<Table>> Handle(GetTablesListQuery request, CancellationToken cancellationToken)
        {
            return await _tableRepository.GetAllWithInclude(x=>x.TableStatus,x=>x.Waiter);
        }
    }
}