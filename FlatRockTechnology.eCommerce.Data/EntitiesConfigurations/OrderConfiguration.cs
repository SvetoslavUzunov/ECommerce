using FlatRockTechnology.eCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatRockTechnology.eCommerce.DataLayer.EntitiesConfigurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
	{
		public void Configure(EntityTypeBuilder<OrderEntity> builder)
		{
			builder
				.HasOne(o => o.CreatedBy)
				.WithMany()
				.HasForeignKey(o => o.CreatedById)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(o => o.ModifiedBy)
				.WithMany()
				.HasForeignKey(o => o.ModifiedById)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}