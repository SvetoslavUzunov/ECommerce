using FlatRockTechnology.eCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatRockTechnology.eCommerce.DataLayer.EntitiesConfigurations
{
	public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
	{
		public void Configure(EntityTypeBuilder<UserEntity> builder)
		{
			builder
				.HasOne(u => u.CreatedBy)
				.WithMany(u => u.CreatedByCollection)
				.HasForeignKey(u => u.CreatedById)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(u => u.ModifiedBy)
				.WithMany(u => u.ModifiedByCollection)
				.HasForeignKey(u => u.ModifiedById)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}