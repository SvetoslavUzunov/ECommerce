using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.Core.Exceptions;
using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.DataLayer
{
	public static class SeedDataManager
	{
		public static async Task<IHost> SeedDataAsync(this IHost host)
		{
			Guid UserAdminId = Guid.NewGuid();
			Guid AddressId = Guid.NewGuid();
			Guid ProductId = Guid.NewGuid();
			Guid OrderId = Guid.NewGuid();
			Guid CategoryId = Guid.NewGuid();

			using (IServiceScope? scope = host.Services.CreateScope())
			{
				try
				{
					using ECommerceDBContext? data = scope.ServiceProvider.GetRequiredService<ECommerceDBContext>();

					RoleManager<RoleEntity> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
					UserManager<UserEntity> userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

					await data.Database.MigrateAsync();

					await SeedRolesAsync(roleManager);
					await SeedUsersAsync(userManager, UserAdminId);
					await AddUsersToRolesAsync(userManager, UserAdminId);

					await SeedAddressesAsync(data, AddressId);
					await SeedUserAddressesAsync(data, UserAdminId, AddressId);
					await SeedStatusAsync(data);
					await SeedProductsAsync(data, ProductId);
					await SeedCategories(data, CategoryId);
					await SeedOrders(data, OrderId);
					await SeedProductCategories(data, ProductId, CategoryId);
					await SeedOrderProducts(data, OrderId, ProductId);

					await data.SaveChangesAsync();
				}
				catch (Exception)
				{
					throw;
				}
			}

			return host;
		}

		private static async Task SeedRolesAsync(RoleManager<RoleEntity> roleManager)
		{
			var roleName = RoleConstants.AdminRole;
			var isRoleExist = await roleManager.FindByNameAsync(roleName);

			if (isRoleExist == null)
			{
				var role = new RoleEntity()
				{
					Name = roleName
				};

				var isRoleCreated = await roleManager.CreateAsync(role);

				if (!isRoleCreated.Succeeded)
				{
					ErrorHandler.ExecuteErrorHandler(isRoleCreated);
				}
			}
		}

		private static async Task SeedUsersAsync(UserManager<UserEntity> userManager, Guid userAdminId)
		{
			var userAdminEmail = UserConstants.AdminUserEmail;
			var isUserExist = await userManager.FindByEmailAsync(userAdminEmail);

			if (isUserExist == null)
			{
				var user = new UserEntity()
				{
					Id = userAdminId,
					UserName = UserConstants.AdminUserName,
					Email = userAdminEmail,
					CreatedById = null
				};

				user.CreatedById = user.Id;

				var isUserCreated = await userManager.CreateAsync(user, UserConstants.AdminUserPassword);

				if (!isUserCreated.Succeeded)
				{
					ErrorHandler.ExecuteErrorHandler(isUserCreated);
				}
			}
		}

		private static async Task AddUsersToRolesAsync(UserManager<UserEntity> userManager, Guid userAdminId)
		{
			var roleName = RoleConstants.AdminRole;

			var user = await userManager.FindByIdAsync(userAdminId.ToString());

			if (user != null)
			{
				var isUserIsInRole = await userManager.IsInRoleAsync(user, roleName);

				if (isUserIsInRole)
				{
					throw new SeedDataException();
				}

				var isUserAddedToRole = await userManager.AddToRoleAsync(user, roleName);

				if (!isUserAddedToRole.Succeeded)
				{
					ErrorHandler.ExecuteErrorHandler(isUserAddedToRole);
				}
			}
		}

		private static async Task SeedAddressesAsync(ECommerceDBContext data, Guid addressId)
		{
			if (!data.Addresses.Any())
			{
				var addresses = new List<AddressEntity>()
				{
					new AddressEntity
					{
						Id = addressId,
						Name = "Milosyrdie 4 street"
					}
				};

				await data.Addresses.AddRangeAsync(addresses);
			}
		}

		private static async Task SeedUserAddressesAsync(ECommerceDBContext data, Guid userAdminId, Guid addressId)
		{
			if (!data.UserAddresses.Any())
			{
				var userAddresses = new List<UserAddressesEntity>()
				{
					new UserAddressesEntity
					{
						UserId = userAdminId,
						AddressId = addressId
					}
				};

				await data.UserAddresses.AddRangeAsync(userAddresses);
			}
		}

		private static async Task SeedStatusAsync(ECommerceDBContext data)
		{
			if (!data.Status.Any())
			{
				var status = new List<StatusEntity>()
				{
					new StatusEntity
					{
						Name = "Derivered"
					},
					new StatusEntity
					{
						Name = "Pending"
					},
					new StatusEntity
					{
						Name = "Canceled"
					}
				};

				await data.Status.AddRangeAsync(status);
			}
		}

		private static async Task SeedProductsAsync(ECommerceDBContext data, Guid productId)
		{
			if (!data.Products.Any())
			{
				var products = new List<ProductEntity>()
				{
					new ProductEntity
					{
						Id = productId,
						Name = "Iphone 4",
						Description = "TestDescription",
						Price = 1440
					}
				};

				await data.Products.AddRangeAsync(products);
			}
		}

		private static async Task SeedCategories(ECommerceDBContext data, Guid categoryId)
		{
			if (!data.Categories.Any())
			{
				var categories = new List<CategoryEntity>()
				{
					new CategoryEntity
					{
						Id = categoryId,
						Name = "Phones"
					}
				};

				await data.Categories.AddRangeAsync(categories);
			}
		}

		private static async Task SeedOrders(ECommerceDBContext data, Guid orderId)
		{
			if (!data.Orders.Any())
			{
				var orders = new List<OrderEntity>()
				{
					new OrderEntity
					{
						Id = orderId
					}
				};

				await data.Orders.AddRangeAsync(orders);
			}
		}

		private static async Task SeedProductCategories(ECommerceDBContext data, Guid productId, Guid categoryId)
		{
			if (!data.ProductCategories.Any())
			{
				var productCategories = new List<ProductCategoriesEntity>()
				{
					new ProductCategoriesEntity
					{
						ProductId = productId,
						CategoryId = categoryId
					}
				};

				await data.ProductCategories.AddRangeAsync(productCategories);
			}
		}

		private static async Task SeedOrderProducts(ECommerceDBContext data, Guid orderId, Guid productId)
		{
			if (!data.OrderProducts.Any())
			{
				var orderProducts = new List<OrderProductsEntity>()
				{
					new OrderProductsEntity
					{
						OrderId = orderId,
						ProductId = productId
					}
				};

				await data.OrderProducts.AddRangeAsync(orderProducts);
			}
		}
	}
}
