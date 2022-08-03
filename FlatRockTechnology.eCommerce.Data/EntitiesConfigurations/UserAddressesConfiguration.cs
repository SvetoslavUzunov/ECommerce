using FlatRockTechnology.eCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatRockTechnology.eCommerce.DataLayer.EntitiesConfigurations
{
	public class UserAddressesConfiguration : IEntityTypeConfiguration<UserAddressesEntity>
	{
		public void Configure(EntityTypeBuilder<UserAddressesEntity> builder)
		{
			builder
				.HasKey(ua => new { ua.UserId, ua.AddressId });
		}
	}
}