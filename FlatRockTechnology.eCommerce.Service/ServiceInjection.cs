using Microsoft.Extensions.DependencyInjection;
using FlatRockTechnology.eCommerce.Service.Services;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Repository;

namespace FlatRockTechnology.eCommerce.Service
{
	public static class ServiceInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			RepositoryInjection.AddRepositories(services);

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<ICategoryService, CategoryService>();

			services.AddScoped<ITokenHandlerService, TokenHandlerService>();
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<ICacheService, CacheService>();

			return services;
		}
	}
}