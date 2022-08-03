using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;

namespace FlatRockTechnology.eCommerce.Core.Contracts.Patterns
{

	public interface IUnitOfWork : IDisposable
	{
		public IUserRepository Users { get; }

		public IProductRepository Products { get; }

		public IOrderRepository Orders { get; }

		public ICategoryRepository Categories { get; }

		public Task<int> CompleteAsync();
	}
}