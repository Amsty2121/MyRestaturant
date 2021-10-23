using System.Collections.Generic;
using Common.Dto.Ingredients;

namespace Common.Dto.IngredientStatuses
{
    public class GetIngredientStatusPagedDto
    {
        public int Id { get; set; }
        public string IngredientStatusName { get; set; }
    }
}