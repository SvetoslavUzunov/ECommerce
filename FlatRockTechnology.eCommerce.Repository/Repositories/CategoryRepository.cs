using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.DataLayer;
using FlatRockTechnology.eCommerce.Core.Entities;

namespace FlatRockTechnology.eCommerce.Repository.Repositories
{
	public class CategoryRepository : GenericRepository<CategoryEntity>, ICategoryRepository
	{
		public CategoryRepository(ECommerceDBContext data) : base(data) { }
	}
}