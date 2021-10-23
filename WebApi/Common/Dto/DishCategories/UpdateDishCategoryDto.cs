using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto.DishCategories
{
	public class UpdateDishCategoryDto
	{
		[StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid DishCategoryName length")]
		public string DishCategoryName { get; set; }
    }
}
