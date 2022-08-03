namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public class UserAddressesEntity
	{
		public UserAddressesEntity()
		{
			this.UserId = Guid.NewGuid();
			this.AddressId = Guid.NewGuid();
		}

		public Guid UserId { get; set; }

		public virtual UserEntity User { get; set; }

		public Guid AddressId { get; set; }

		public virtual AddressEntity Address { get; set; }
	}
}