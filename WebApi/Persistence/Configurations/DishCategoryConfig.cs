using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DishCategoryConfig
    {
        public void Configure(EntityTypeBuilder<DishCategory> builder)
        {
            builder.Property(x => x.DishCategoryName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);
        }
    }
}
