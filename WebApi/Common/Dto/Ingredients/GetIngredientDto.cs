﻿namespace Common.Dto.Ingredients
{
	public class GetIngredientDto
	{
        public int Id {get; set; }
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        public int IngredientStatusId { get; set; }
        public string IngredientStatusName { get; set; }
    }
}
