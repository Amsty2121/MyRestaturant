using System.ComponentModel.DataAnnotations;

namespace Common.Dto.OrderStatuses
{
    public class InsertOrderStatusDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid OrderStatusName length")]
        public string OrderStatusName { get; set; }
    }
}