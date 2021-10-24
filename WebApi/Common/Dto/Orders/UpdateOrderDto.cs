using System.ComponentModel.DataAnnotations;

namespace Common.Dto.Orders
{
	public class UpdateOrderDto
	{
        [Range(1,100)]
        public int OrderNrPortions { get; set; }

        [StringLength(500, MinimumLength = 0, ErrorMessage = "Invalid OrderDescription length")]
        public string OrderDescription { get; set; }
        public int TableId { get; set; }
        public int OrderStatusId { get; set; }
        public int DishId { get; set; }
        public int? KitchenerId { get; set; }
    }
}
