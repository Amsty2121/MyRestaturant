namespace Common.Dto.Ingredients
{
	public class InsertedIngredientDto
	{
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        public int IngredientStatusId { get; set; }
    }
}
