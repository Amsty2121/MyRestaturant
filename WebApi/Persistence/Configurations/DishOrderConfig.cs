using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DishOrderConfig : IEntityTypeConfiguration<DishOrder>
    {
        public void Configure(EntityTypeBuilder<DishOrder> builder)
        {
            builder.Property(x => x.DishId)
                .IsRequired();

            builder.Property(x => x.OrderId)
                .IsRequired();

        }
    }
}