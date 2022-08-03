using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.DataLayer;
using FlatRockTechnology.eCommerce.Core.Entities;

namespace FlatRockTechnology.eCommerce.Repository.Repositories
{
	public class UserRepository : GenericRepository<UserEntity>, IUserRepository
	{
		public UserRepository(ECommerceDBContext data) : base(data) { }
	}
}