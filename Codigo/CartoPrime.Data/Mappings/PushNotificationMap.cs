using CartoPrime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TM.Utils.Bases;

namespace CartoPrime.Data.Mappings
{
    public class PushNotificationMap : MapBase<PushNotification>
	{
		public override void Configure(EntityTypeBuilder<PushNotification> builder)
		{
			builder.ToTable("push_notification");

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
