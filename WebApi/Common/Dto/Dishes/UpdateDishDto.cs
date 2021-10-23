using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto.Dishes
{
	public class UpdateDishDto
	{
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid DishName length")]
        public string DishName { get; set; }

        [Range(1, 1000000)]
        public int DishPrice { get; set; }

        [StringLength(500, MinimumLength = 0, ErrorMessage = "Invalid DishDescription length")]
        public string DishDescription { get; set; }
        public int DishStatusId { get; set; }
        public int DishCategoryId { get; set; }
        public ICollection<int> IngredientsId { get; set; }
    }
}
