using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Ingredients.Commands.DeleteIngredient;
using Application.Ingredients.Commands.InsertIngredient;
using Application.Ingredients.Commands.UpdateIngredient;
using Application.Ingredients.Queries.GetIngredientById;
using Application.Ingredients.Queries.GetIngredientPaged;
using Application.Ingredients.Queries.GetIngredientsList;
using Application.IngredientStatuses.Queries.GetStatusByIngredientId;
using Common.Dto.Ingredients;
using Common.Dto.IngredientStatuses;
using Common.Models.PagedRequest;
using Domain.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public IngredientController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetIngredient()
        {

            var ingredients = await _mediator.Send(new GetIngredientsListQuery());
            var results = ingredients.Select(x => _mapper.Map<GetIngredientListDto>(x));

            return Ok(results);
        }

        [HttpGet("{ingredientId}")]
        public async Task<IActionResult> GetIngredientById(int ingredientId)
        {
            var queryIngredient = new GetIngredientByIdQuery() { IngredientId = ingredientId };
            var ingredientWithStatus = await _mediator.Send(queryIngredient);

            var result = _mapper.Map<GetIngredientDto>(ingredientWithStatus);

            return Ok(result);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("paginated-search")]
        public async Task<IActionResult> GetIngredientsPaged([FromBody] PagedRequest pagedRequest)
        {
            var query = new GetIngredientsPagedQuery() { PagedRequest = pagedRequest };
            var ingredients = await _mediator.Send(query);
            var ingredientsResult = _mapper.Map<PaginatedResult<GetIngredientPagedDto>>(ingredients);

            foreach (var ingredient in ingredientsResult.Items)
            {
                ingredient.IngredientStatus = (await _mediator.Send(new GetStatusByIngredientIdQuery() { IngredientId = ingredient.Id}));
            }

            return Ok(ingredientsResult);
        }

        [HttpPost]
        public async Task<IActionResult> InsertIngredient(InsertIngredientDto dto)
        {
            InsertIngredientCommand command = new InsertIngredientCommand() { Dto = dto };
            var ingredient = await _mediator.Send(command);
            var result = _mapper.Map<InsertedIngredientDto>(ingredient);
            return CreatedAtAction(nameof(GetIngredient), new { id = result.Id }, result);
        }

        [HttpDelete("{ingredientId}")]
        public async Task<IActionResult> DeleteIngredient(int ingredientId)
        {
            DeleteIngredientCommand command = new DeleteIngredientCommand() { IngredientId = ingredientId };
            var isDeleted = await _mediator.Send(command);

            return isDeleted ? NoContent() : NotFound();
        }

        [HttpPatch("{ingredientId}")]
        public async Task<IActionResult> UpdateIngredient(int ingredientId, [FromBody] UpdateIngredientDto dto)
        {

            UpdateIngredientCommand command = new UpdateIngredientCommand()
            {
                Id = ingredientId,
                Dto = dto
            };
            var ingredient = await _mediator.Send(command);

            var result = _mapper.Map<UpdatedIngredientDto>(ingredient);

            return Ok(result);
        }
    }
}