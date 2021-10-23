using System.Collections.Generic;

namespace Domain.Entities
{
    public class IngredientStatus:BaseEntity
    {
        public string IngredientStatusName { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
