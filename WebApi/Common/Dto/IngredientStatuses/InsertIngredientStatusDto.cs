using System.ComponentModel.DataAnnotations;

namespace Common.Dto.IngredientStatuses
{
	public class InsertIngredientStatusDto
	{
		[Required]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid IngredientStatusName length")]
		public string IngredientStatusName { get; set; }
	}
}
