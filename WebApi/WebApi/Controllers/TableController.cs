using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Tables.Commands.DeleteTable;
using Application.Tables.Commands.InsertTable;
using Application.Tables.Commands.UpdateTable;
using Application.Tables.Queries.GetTableById;
using Application.Tables.Queries.GetTableList;
using Common.Dto.Tables;

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
            var results = tables.Select(x => _mapper.Map<TablesWithStatusesAndWaiters>(x));

            return Ok(results);
        }

        [HttpGet("{tableId}")]
        public async Task<IActionResult> GetTableById(int tableId)
        {
            var queryTable = new GetTableByIdQuery() { TableId = tableId };
            TableWithStatusWaiterAndOrders tableWithStatusWaiterAndOrders = await _mediator.Send(queryTable);

            var result = _mapper.Map<GetTableDto>(tableWithStatusWaiterAndOrders);

            return Ok(result);
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