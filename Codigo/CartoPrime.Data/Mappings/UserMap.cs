using CartoPrime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TM.Utils.Bases;

namespace CartoPrime.Data.Mappings
{
    public class UserMap : MapBase<User>
	{
		public override void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("user");

			builder.HasKey(c => c.Id);


			builder.Property(c => c.Removed)
				.IsRequired()
				.HasColumnName("Removed");

			builder.Property(c => c.InputDate)
				.IsRequired()
				.HasColumnName("Created");

			builder.Property(c => c.UpdateDate)
				.HasColumnName("Updated");

		}
	}
}
