using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.TableStatuses;
using Application.TableStatuses.Queries.GetTableStatusById;
using Application.TableStatuses.Queries.GetTableStatusesList;
using Common.Dto.TableStatuses;
using Domain.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableStatusController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TableStatusController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTableStatus()
        {
            var tableStatus = await _mediator.Send(new GetTableStatusesListQuery());
            var result = tableStatus.Select(x => _mapper.Map<GetTableStatusesListDto>(x));
            return Ok(result);
        }

        [HttpGet("{tableStatusId}")]
        public async Task<IActionResult> GetTableStatusById(int tableStatusId)
        {
            var query = new GetTableStatusByIdQuery() { TableStatusId = tableStatusId };
            TableStatus tableStatus = await _mediator.Send(query);
            var result = _mapper.Map<GetTableStatusDto>(tableStatus);

            return Ok(result);
        }

        [HttpPost] 
        public async Task<IActionResult> InsertTableStatus(InsertTableStatusDto dto)
        {
            InsertTableStatusCommand command = new InsertTableStatusCommand() { Dto = dto };
            var tableStatus = await _mediator.Send(command);
            var result = _mapper.Map<InsertedTableStatusDto>(tableStatus);
            return CreatedAtAction(nameof(GetTableStatus), new { id = result.Id }, result);
        }
    }
}