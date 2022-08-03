using FlatRockTechnology.eCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatRockTechnology.eCommerce.DataLayer.EntitiesConfigurations
{
	public class UserRolesConfiguration : IEntityTypeConfiguration<UserRolesEntity>
	{
		public void Configure(EntityTypeBuilder<UserRolesEntity> builder)
		{
			builder
				.HasKey(ur => new { ur.UserId, ur.RoleId });
		}
	}
}