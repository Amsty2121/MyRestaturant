using System.Collections.Generic;

namespace Domain.Entities
{
    public class DishStatus: BaseEntity
    {
        public string DishStatusName { get; set; }
        public ICollection<Dish> Dishes { get; set; }
    }
}
    