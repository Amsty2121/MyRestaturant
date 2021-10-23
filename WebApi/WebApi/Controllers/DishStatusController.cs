using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.DishStatuses;
using Application.DishStatuses.Queries.GetDishStatusById;
using Application.DishStatuses.Queries.GetDishStatusesList;
using Common.Dto.DishStatuses;
using Domain.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class DishStatusController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DishStatusController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDishStatus()
        {
            var dishStatus = await _mediator.Send(new GetDishStatusesListQuery());
            var result = dishStatus.Select(x => _mapper.Map<GetDishStatusesListDto>(x));
            return Ok(result);
        }

        [HttpGet("{dishStatusId}")]
        public async Task<IActionResult> GetDishStatusById(int dishStatusId)
        {
            var query = new GetDishStatusByIdQuery() { DishStatusId = dishStatusId };
            DishStatus dishStatus = await _mediator.Send(query);
            var result = _mapper.Map<GetDishStatusDto>(dishStatus);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDishStatus(InsertDishStatusDto dto)
        {
            var command = new InsertDishStatusCommand() { Dto = dto };
            var dishStatus = await _mediator.Send(command);
            var result = _mapper.Map<InsertedDishStatusDto>(dishStatus);
            return CreatedAtAction(nameof(GetDishStatus), new { id = result.Id }, result);
        }
    }
}