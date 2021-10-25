﻿using System.Collections.Generic;

namespace Domain.Entities
{
    public class Dish : BaseEntity
    {
        public string DishName { get; set; }
        public int DishPrice { get; set; }
        public string DishDescription { get; set; }
        public int DishCategoryId { get; set; }
        public DishCategory DishCategory { get; set; }
        public int DishStatusId { get; set; }
        public DishStatus DishStatus { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
