using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.IngredientStatuses;
using Application.IngredientStatuses.Queries.GetIngredientStatusById;
using Application.IngredientStatuses.Queries.GetIngredientStatusesList;
using Common.Dto.IngredientStatuses;
using Domain.Entities;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IngredientStatusController : BaseController
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public IngredientStatusController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetIngredientStatus()
		{
			var ingredientStatus = await _mediator.Send(new GetIngredientStatusesListQuery());
			var result = ingredientStatus.Select(x => _mapper.Map<GetIngredientStatusesListDto>(x));
			return Ok(result);
		}

		[HttpGet("{ingredientStatusId}")]
		public async Task<IActionResult> GetIngredientStatusById(int ingredientStatusId)
		{
			var query = new GetIngredientStatusByIdQuery() { IngredientStatusId  = ingredientStatusId };
			IngredientStatus ingredientStatus = await _mediator.Send(query);
			var result = _mapper.Map<GetIngredientStatusDto>(ingredientStatus);

			return Ok(result);
		}

        [HttpPost]
        public async Task<IActionResult> InsertIngredientStatus(InsertIngredientStatusDto dto)
        {
			InsertIngredientStatusCommand command = new InsertIngredientStatusCommand() { Dto = dto };
            var ingredientStatus = await _mediator.Send(command);
            var result = _mapper.Map<InsertedIngredientStatusDto>(ingredientStatus);
            return CreatedAtAction(nameof(GetIngredientStatus), new { id = result.Id }, result);
        }
	}
}
