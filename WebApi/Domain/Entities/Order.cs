using System.Collections.Generic;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public int OrderNrPortions { get; set; }
        public string OrderDescription { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; }
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public int? KitchenerId { get; set; }
        public Kitchener Kitchener { get; set; }
    }
}
