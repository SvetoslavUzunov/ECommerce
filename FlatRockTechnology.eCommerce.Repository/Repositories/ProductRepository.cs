using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.DataLayer;
using FlatRockTechnology.eCommerce.Core.Entities;

namespace FlatRockTechnology.eCommerce.Repository.Repositories
{
	public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
	{
		public ProductRepository(ECommerceDBContext data) : base(data) { }
	}
}