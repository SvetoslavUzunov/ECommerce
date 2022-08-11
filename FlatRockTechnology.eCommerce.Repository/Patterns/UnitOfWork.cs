using FlatRockTechnology.eCommerce.Core.Contracts.Patterns;
using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.DataLayer;

namespace FlatRockTechnology.eCommerce.Repository.Patterns
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ECommerceDBContext data;

		public UnitOfWork(ECommerceDBContext data,
			IUserRepository users,
			IProductRepository products,
			IOrderRepository orders,
			ICategoryRepository categories)
		{
			this.data = data;
			Users = users;
			Products = products;
			Orders = orders;
			Categories = categories;
		}

		public IUserRepository Users { get; }

		public IProductRepository Products { get; }

		public IOrderRepository Orders { get; }

		public ICategoryRepository Categories { get; }

		public async Task<int> CompleteAsync()
			=> await data.SaveChangesAsync();

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				data.Dispose();
			}
		}
	}
}