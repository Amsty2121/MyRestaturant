using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Dishes.Queries.GetDishByOrderId;
using Application.Dishes.Queries.GetDishesByTableId;
using Application.Kitcheners.Queries.GetKitchenerByOrderId;
using Application.Orders.Queries.GetOrdersByTableId;
using Application.Orders.Queries.GetOrdersPaged;
using Application.OrderStatuses.Queries.GetStatusByOrderId;
using Application.Tables.Commands.DeleteTable;
using Application.Tables.Commands.InsertTable;
using Application.Tables.Commands.UpdateTable;
using Application.Tables.Queries.GetTableById;
using Application.Tables.Queries.GetTableByOrderId;
using Application.Tables.Queries.GetTableList;
using Application.Tables.Queries.GetTablesPaged;
using Application.TableStatuses.Queries.GetStatusByTableId;
using Application.Waiters.Queries.GetWaiterByOrderId;
using Application.Waiters.Queries.GetWaiterByTableId;
using Common.Dto.Tables;
using Common.Models.PagedRequest;
using Domain.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TableController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTables()
        {

            var tables = await _mediator.Send(new GetTablesListQuery());
            var results = tables.Select(x => _mapper.Map<GetTableListDto>(x));

            return Ok(results);
        }

        [HttpGet("{tableId}")]
        public async Task<IActionResult> GetTableById(int tableId)
        {
            var queryTable = new GetTableByIdQuery() { TableId = tableId };
            var table = await _mediator.Send(queryTable);

            var result = _mapper.Map<GetTableDto>(table);

            return Ok(result);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("paginated-search")]
        public async Task<IActionResult> GetTablesPaged([FromBody] PagedRequest pagedRequest)
        {
            var query = new GetTablePagedQuery() { PagedRequest = pagedRequest };
            var tables = await _mediator.Send(query);
            var tablesResult = _mapper.Map<PaginatedResult<GetTablePagedDto>>(tables);

            foreach (var table in tablesResult.Items)
            {
                table.TableStatus = (await _mediator.Send(new GetStatusByTableIdQuery() { TableId = table.Id }));
                table.Waiter = (await _mediator.Send(new GetWaiterByTableIdQuery() { TableId = table.Id }));
            }

            var a = tablesResult;
            return Ok(a);
        }

        [HttpPost]
        public async Task<IActionResult> InsertTable(InsertTableDto dto)
        {
            var command = new InsertTableCommand() { Dto = dto };
            var table = await _mediator.Send(command);
            var result = _mapper.Map<InsertedTableDto>(table);
            return CreatedAtAction(nameof(GetTables), new { id = result.Id }, result);
        }

        [HttpDelete("{tableId}")]
        public async Task<IActionResult> DeleteTable(int tableId)
        {
            var command = new DeleteTableCommand() { TableId = tableId };
            var isDeleted = await _mediator.Send(command);

            return isDeleted ? NoContent() : NotFound();
        }

        [HttpPatch("{tableId}")]
        public async Task<IActionResult> UpdateTable(int tableId, [FromBody] UpdateTableDto dto)
        {

            var command = new UpdateTableCommand()
            {
                Id = tableId,
                Dto = dto
            };
            var table = await _mediator.Send(command);

            var result = _mapper.Map<UpdatedTableDto>(table);

            return Ok(result);
        }
    }
}