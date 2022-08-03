using FlatRockTechnology.eCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatRockTechnology.eCommerce.DataLayer.EntitiesConfigurations
{
	public class OrderProductsConfiguration : IEntityTypeConfiguration<OrderProductsEntity>
	{
		public void Configure(EntityTypeBuilder<OrderProductsEntity> builder)
		{
			builder
				.HasKey(op => new { op.OrderId, op.ProductId });
		}
	}
}