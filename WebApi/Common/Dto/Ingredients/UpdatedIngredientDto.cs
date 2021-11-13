namespace Common.Dto.Ingredients
{
	public class UpdatedIngredientDto
	{
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        public int IngredientStatusId { get; set; }

    }
}
