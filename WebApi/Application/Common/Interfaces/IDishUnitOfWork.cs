using Domain.Entities;

namespace Application.Common.Interfaces
{
	public interface IDishUnitOfWork
	{
		public IGenericRepository<Dish> DishRepository { get; }
		public IGenericRepository<DishIngredient> DishIngredientRepository { get; }

        public void Save();
	}
}
