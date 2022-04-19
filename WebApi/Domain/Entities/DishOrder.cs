using System.Collections.Generic;

namespace Domain.Entities
{
    public class DishOrder: BaseEntity
    {
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
