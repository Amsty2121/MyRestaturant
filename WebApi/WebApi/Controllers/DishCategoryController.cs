using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.DishCategories.Commands.DeleteDishCategory;
using Application.DishCategories.Commands.InsertDishCategory;
using Application.DishCategories.Commands.UpdateDishCategory;
using Application.DishCategories.Queries.GetDishCategoryById;
using Application.DishCategories.Queries.GetDishCategoriesList;
using Common.Dto.DishCategories;
using Domain.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishCategoryController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DishCategoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDishCategory()
        {
            var dishCategory = await _mediator.Send(new GetDishCategoriesListQuery());
            var result = dishCategory.Select(x => _mapper.Map<GetDishCategoriesListDto>(x));
            return Ok(result);
        }

        [HttpGet("{dishCategoryId}")]
        public async Task<IActionResult> GetDishCategoryById(int dishCategoryId)
        {
            var query = new GetDishCategoryByIdQuery() { DishCategoryId = dishCategoryId };
            DishCategory dishCategory = await _mediator.Send(query);
            var result = _mapper.Map<GetDishCategoryDto>(dishCategory);

            return Ok(result);
        }

        [HttpPost] 
        public async Task<IActionResult> InsertDishCategory(InsertDishCategoryDto dto)
        {
            InsertDishCategoryCommand command = new InsertDishCategoryCommand() { Dto = dto };
            var dishCategory = await _mediator.Send(command);
            var result = _mapper.Map<InsertedDishCategoryDto>(dishCategory);
            return CreatedAtAction(nameof(GetDishCategory), new { id = result.Id }, result);
        }

        [HttpDelete("{dishCategoryId}")]
        public async Task<IActionResult> DeleteDishCategory(int dishCategoryId)
        {
            DeleteDishCategoryCommand command = new DeleteDishCategoryCommand() { DishCategoryId = dishCategoryId };
            var isDeleted = await _mediator.Send(command);

            return isDeleted ? NoContent() : NotFound();
        }

        [HttpPatch("{dishCategoryId}")]
        public async Task<IActionResult> UpdateDishCategory(int dishCategoryId, [FromBody] UpdateDishCategoryDto dto)
        {

            UpdateDishCategoryCommand command = new UpdateDishCategoryCommand()
            {
                DishCategoryId = dishCategoryId,
                DishCategoryName = dto.DishCategoryName
                
            };
            var dishCategory = await _mediator.Send(command);

            var result = _mapper.Map<UpdatedDishCategoryDto>(dishCategory);

            return Ok(result);
        }
    }
}