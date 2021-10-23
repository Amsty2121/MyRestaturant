namespace Domain.Entities
{
    public class DishIngredient : BaseEntity
    {
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public string DishIngredientDescription { get; set; }
    }
}
