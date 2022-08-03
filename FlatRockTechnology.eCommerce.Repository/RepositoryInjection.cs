using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.Repository.Repositories;
using FlatRockTechnology.eCommerce.DataLayer;
using FlatRockTechnology.eCommerce.Core.Contracts.Patterns;

namespace FlatRockTechnology.eCommerce.Repository
{
	public static class RepositoryInjection
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddDbContext<ECommerceDBContext>(options => options
					.UseSqlServer(new DBConnectionString().GetDbConnectionString));

			return services;
		}
	}
}