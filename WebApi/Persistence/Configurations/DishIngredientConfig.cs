using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DishIngredientConfig : IEntityTypeConfiguration<DishIngredient>
    {
        public void Configure(EntityTypeBuilder<DishIngredient> builder)
        {
            builder.HasKey(x => new { x.DishId, x.IngredientId });

            builder.HasOne(x => x.Dish)
                .WithMany(x => x.DishIngredients)
                .HasForeignKey(x => x.DishId);

            builder.HasOne(x => x.Ingredient)
                .WithMany(x => x.DishIngredients)
                .HasForeignKey(x => x.IngredientId);
        }
    }
}