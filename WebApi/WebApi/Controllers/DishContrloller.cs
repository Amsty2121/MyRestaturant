using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.DishCategories.Queries.GetCategoryByDishId;
using Application.Dishes;
using Application.Dishes.Commands.DeleteDish;
using Application.Dishes.Commands.InsertDish;
using Application.Dishes.Commands.UpdateDish;
using Application.Dishes.Queries.GetDishById;
using Application.Dishes.Queries.GetDishesList;
using Application.Dishes.Queries.GetDishesPaged;
using Application.DishStatuses.Queries.GetStatusByDishId;
using Application.Ingredients.Queries.GetIngredientsByDishId;
using Common.Dto.Dishes;
using Common.Models.PagedRequest;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DishController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDishes()
        {

            var dishes = await _mediator.Send(new GetDishesListQuery());
            var results = dishes.Select(x => _mapper.Map<GetDishListDto>(x));

            return Ok(results);
        }

        [HttpGet("{dishId}")]
        public async Task<IActionResult> GetDishById(int dishId)
        {
            var queryDish = new GetDishByIdQuery() { DishId = dishId };
            var dishWithStatus = await _mediator.Send(queryDish);

            var result = _mapper.Map<GetDishDto>(dishWithStatus);

            return Ok(result);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("paginated-search")]
        public async Task<IActionResult> GetDishesPaged([FromBody] PagedRequest pagedRequest)
        {
            var query = new GetDishPagedQuery() { PagedRequest = pagedRequest };
            var dishes = await _mediator.Send(query);
            var dishesResult = _mapper.Map<PaginatedResult<GetDishPagedDto>>(dishes);

            foreach (var dish in dishesResult.Items)
            {
                dish.DishStatus = (await _mediator.Send(new GetStatusByDishIdQuery() { DishId = dish.Id }));
                dish.DishCategory = (await _mediator.Send(new GetCategoryByDishIdQuery() { DishId = dish.Id }));
                dish.Ingredients =
                    (await _mediator.Send(new GetIngredientsByDishIdQuery() { DishId = dish.Id }))
                    .GroupBy(x=>x.Id).Select(y=>y.FirstOrDefault()).ToList();
            }

            var a = dishesResult;
            return Ok(a);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDish(InsertDishDto dto)
        {
            var command = new InsertDishCommand() { Dto = dto };
            var dish = await _mediator.Send(command);
            var result = _mapper.Map<InsertedDishDto>(dish);
            return CreatedAtAction(nameof(GetDishes), new { id = result.Id }, result);
        }

        [HttpDelete("{dishId}")]
        public async Task<IActionResult> DeleteDish(int dishId)
        {
            var command = new DeleteDishCommand() { DishId = dishId };
            var isDeleted = await _mediator.Send(command);

            return isDeleted ? NoContent() : NotFound();
        }

        [HttpPatch("{dishId}")]
        public async Task<IActionResult> UpdateDish(int dishId, [FromBody] UpdateDishDto dto)
        {

            var command = new UpdateDishCommand()
            {
                Id = dishId,
                Dto = dto
            };
            var dish = await _mediator.Send(command);

            var result = _mapper.Map<UpdatedDishDto>(dish);

            return Ok(result);
        }
    }
}