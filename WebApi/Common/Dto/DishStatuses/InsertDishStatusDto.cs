using System.ComponentModel.DataAnnotations;

namespace Common.Dto.DishStatuses
{
    public class InsertDishStatusDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid DishStatusName length")]
        public string DishStatusName { get; set; }
    }
}