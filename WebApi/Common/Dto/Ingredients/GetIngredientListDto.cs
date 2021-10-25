namespace Common.Dto.Ingredients
{
	public class GetIngredientListDto
	{
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        public int IngredientStatusId { get; set; }
    }
}
