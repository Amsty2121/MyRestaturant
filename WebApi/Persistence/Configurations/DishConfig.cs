using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DishConfig : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {

            builder.Property(x => x.DishDescription)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(500);

            builder.Property(x => x.DishName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(x => x.DishPrice)
                .IsRequired();
        }
    }
}