namespace Common.Dto.Orders
{
	public class InsertedOrderDto
	{
        public int Id { get; set; }
        public int OrderNrPortions { get; set; }
        public string OrderDescription { get; set; }
        public int TableId { get; set; }
        public int OrderStatusId { get; set; }
        public int DishId { get; set; }
    }
}
