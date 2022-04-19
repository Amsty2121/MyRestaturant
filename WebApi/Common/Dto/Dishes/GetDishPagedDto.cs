using System.Collections.Generic;
using Common.Dto.Ingredients;
using Common.Dto.IngredientStatuses;
using Domain.Entities;

namespace Common.Dto.Dishes
{
    public class GetDishPagedDto
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public int DishPrice { get; set; }
        public string DishDescription { get; set; }
        public DishStatus DishStatus { get; set; }
        public DishCategory DishCategory { get; set; }
        public ICollection<GetIngredientDto> Ingredients { get; set; }
        /*public ICollection<Order> Orders { get; set; }*/
    }
}