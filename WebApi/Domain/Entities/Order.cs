using System.Collections.Generic;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public int OrderNrPortions { get; set; }
        public string OrderDescription { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DishOrder DishOrder { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; }
        public int? KitchenerId { get; set; }
        public Kitchener Kitchener { get; set; }

    }
}