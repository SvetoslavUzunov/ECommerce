using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlatRockTechnology.eCommerce.Core.Entities;

namespace FlatRockTechnology.eCommerce.DataLayer.EntitiesConfigurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
	{
		public void Configure(EntityTypeBuilder<CategoryEntity> builder)
		{
			builder
				.HasOne(c => c.ParentCategory)
				.WithMany(c => c.ParentCategories)
				.HasForeignKey(c => c.ParentCategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.CreatedBy)
				.WithMany()
				.HasForeignKey(c => c.CreatedById)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.ModifiedBy)
				.WithMany()
				.HasForeignKey(c => c.ModifiedById)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}