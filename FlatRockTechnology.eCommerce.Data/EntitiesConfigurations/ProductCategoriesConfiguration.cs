using FlatRockTechnology.eCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatRockTechnology.eCommerce.DataLayer.EntitiesConfigurations
{
	public class ProductCategoriesConfiguration : IEntityTypeConfiguration<ProductCategoriesEntity>
	{
		public void Configure(EntityTypeBuilder<ProductCategoriesEntity> builder)
		{
			builder
				.HasKey(pc => new { pc.ProductId, pc.CategoryId });
		}
	}
}