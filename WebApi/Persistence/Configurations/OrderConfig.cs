using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.OrderDescription)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(500);

            builder.Property(x => x.OrderNrPortions)
                .IsRequired();
        }
    }
}