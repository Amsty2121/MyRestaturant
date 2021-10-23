using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto.Ingredients
{
	public class UpdateIngredientDto
	{
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid IngredientName length")]
        public string IngredientName { get; set; }

        [StringLength(500, MinimumLength = 0, ErrorMessage = "Invalid IngredientDescription length")]
        public string IngredientDescription { get; set; }
        public int IngredientStatusId { get; set; }
    }
}
