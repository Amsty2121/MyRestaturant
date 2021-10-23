using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class TableConfig : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {

            builder.Property(x => x.TableDescription)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(500);
        }
    }
}