using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.DataLayer.EntitiesConfigurations;

namespace FlatRockTechnology.eCommerce.DataLayer
{
	public class ECommerceDBContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
	{
		public ECommerceDBContext(DbContextOptions<ECommerceDBContext> options) : base(options) { }

		public override DbSet<UserEntity> Users { get; set; }

		public DbSet<AddressEntity> Addresses { get; set; }

		public DbSet<UserAddressesEntity> UserAddresses { get; set; }

		public DbSet<CategoryEntity> Categories { get; set; }

		public DbSet<ProductEntity> Products { get; set; }

		public DbSet<ProductCategoriesEntity> ProductCategories { get; set; }

		public DbSet<OrderEntity> Orders { get; set; }

		public DbSet<OrderProductsEntity> OrderProducts { get; set; }

		public override DbSet<RoleEntity> Roles { get; set; }

		public DbSet<StatusEntity> Status { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new UserAddressesConfiguration());
			builder.ApplyConfiguration(new OrderConfiguration());
			builder.ApplyConfiguration(new ProductConfiguration());
			builder.ApplyConfiguration(new OrderProductsConfiguration());
			builder.ApplyConfiguration(new CategoryConfiguration());
			builder.ApplyConfiguration(new ProductCategoriesConfiguration());
			builder.ApplyConfiguration(new UserRolesConfiguration());

			base.OnModelCreating(builder);
		}
	}
}