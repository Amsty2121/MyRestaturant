using System.Collections.Generic;
using Application.Common.Interfaces;
using Common.Models.PagedRequest;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Kitcheners.Queries.GetKitchenerByOrderId;
using Common.Dto.Dishes;
using Common.Dto.Kitcheners;
using Common.Dto.Orders;
using Common.Dto.Waiters;


namespace Application.Tables.Queries.GetTablesPaged
{
    public class GetTablePagedDto
    {
        public int Id { get; set; }
        public string TableDescription { get; set; }
        public TableStatus TableStatus { get; set; }
        public GetWaiterDto Waiter { get; set; }
    }


    public class GetTablePagedQuery : IRequest<PaginatedResult<Table>>
    {
        public PagedRequest PagedRequest { get; set; }
    }

    public class GetTablesPagedQueryHandler : IRequestHandler<GetTablePagedQuery, PaginatedResult<Table>>
    {
        private readonly IGenericRepository<Table> _tableRepository;

        public GetTablesPagedQueryHandler(IGenericRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<PaginatedResult<Table>> Handle(GetTablePagedQuery request, CancellationToken cancellationToken)
        {
            var a = await _tableRepository.GetPagedData<Table>(request.PagedRequest);
            return a;
        }
    }
}