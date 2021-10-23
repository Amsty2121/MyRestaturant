namespace Common.Dto.Dishes
{
	public class GetDishListDto
	{
        public int Id { get; set; }
        public string DishName { get; set; }
        public int DishPrice { get; set; }
        public string DishDescription { get; set; }
        public int DishCategoryId { get; set; }
        public int DishStatusId { get; set; }
    }
}
