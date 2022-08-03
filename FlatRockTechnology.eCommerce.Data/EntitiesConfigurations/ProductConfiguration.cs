using FlatRockTechnology.eCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatRockTechnology.eCommerce.DataLayer.EntitiesConfigurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
	{
		public void Configure(EntityTypeBuilder<ProductEntity> builder)
		{
			builder
				.HasOne(p => p.CreatedBy)
				.WithMany()
				.HasForeignKey(p => p.CreatedById)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(p => p.ModifiedBy)
				.WithMany()
				.HasForeignKey(p => p.ModifiedById)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}