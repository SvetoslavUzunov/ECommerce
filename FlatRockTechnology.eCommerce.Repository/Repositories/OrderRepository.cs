using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.DataLayer;

namespace FlatRockTechnology.eCommerce.Repository.Repositories
{
	public class OrderRepository : GenericRepository<OrderEntity>, IOrderRepository
	{
		public OrderRepository(ECommerceDBContext data) : base(data) { }
	}
}