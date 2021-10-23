using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
	public class UserDetailsConfig : IEntityTypeConfiguration<UserDetails>
	{
		public void Configure(EntityTypeBuilder<UserDetails> builder)
		{
			builder.Property(x => x.FirstName)
				.IsRequired()
				.IsUnicode()
				.HasMaxLength(50);

			builder.Property(x => x.LastName)
				.IsRequired()
				.IsUnicode()
				.HasMaxLength(50);

        }
	}
}
