using System;
using System.Collections.Generic;

namespace Common.Dto.Orders
{
	public class GetOrderDto
	{
		public int Id { get; set; }
        public int OrderNrPortions { get; set; }
        public string OrderDescription { get; set; }
        public int WaiterId { get; set; }
        public string WaiterName { get; set; }
        public int TableId { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
        public int DishId { get; set; }
        public string DishName { get; set; }
        public int? KitchenerId { get; set; }
        public string KitchenerName { get; set; }
    }
}
