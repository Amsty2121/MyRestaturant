using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class IngredientConfig : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {

            builder.Property(x => x.IngredientDescription)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(500);

            builder.Property(x => x.IngredientName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

        }
    }
}