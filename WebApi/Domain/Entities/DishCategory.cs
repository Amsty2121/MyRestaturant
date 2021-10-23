using System.Collections.Generic;

namespace Domain.Entities
{
    public class DishCategory : BaseEntity
    {
        public string DishCategoryName { get; set; }
        public ICollection<Dish> Dishes { get; set; }
    }
}
