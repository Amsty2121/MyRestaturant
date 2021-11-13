    using System.Collections.Generic;

namespace Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        public int IngredientStatusId { get; set; }
        public IngredientStatus IngredientStatus { get; set; }
        public ICollection<DishIngredient> DishIngredients { get; set; }
    }
}
